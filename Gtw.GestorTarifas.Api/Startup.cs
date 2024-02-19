using BancoMaster.LogManager.Extensions;
using BancoMaster.LogManager.Middleware;
using BancoMaster.LogManager.Models;
using Gtw.GestorTarifas.Api.Extensions;
using Gtw.GestorTarifas.Domain.Aws;
using Gtw.GestorTarifas.Domain.Constantes;
using Gtw.GestorTarifas.Domain.Exceptions;
using Gtw.GestorTarifas.Service.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Gtw.GestorTarifas.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices();

            services.AddControllers(c => c.Conventions.Add(new ApiExplorerGroupPerVersionConvention()))
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.Name);

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API GTW Gestor Tarifas - Banco Master",
                        Version = "v1",
                        Description = "API GTW Gestor Tarifas, disponibilização de rotas da API Gestor Tarifas para consumo do APP.",
                        Contact = new OpenApiContact
                        {
                            Name = "Manual de Desenvolvimento",
                            Url = new Uri("https://bmaxima.sharepoint.com/:w:/s/TI/ET3e49--O6lItd96cBluB7ABn5bxezFpEDvRnu0HPeQtOA?e=8dcFju")
                        },
                        TermsOfService = new Uri("https://www.bancomaster.com.br/outras-informacoes/termos-de-uso"),
                        License = new OpenApiLicense
                        {
                            Name = "Banco Master",
                            Url = new Uri("https://www.bancomaster.com.br"),
                        }
                    });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Token de autenticação Bearer. Exemplo: \"Bearer {token}\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Auth
            services.AddHttpContextAccessor();
            AddCognitoAuthentication(services);

            ConfigurarLog(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            // Middleware de correlationID para gerar UUID integrado entre Sentry, MongoDB, Response API e Log Kibana
            app.UseMiddleware<CorrelationIdMiddleware>();

            //// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI();

            app.UseHsts();

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    string message;
                    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    if (ex == null)
                    {
                        return;
                    }
                    else if (ex is MasterException legacyEx)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;

                        var error = new { legacyEx.CodigoErro, Mensagem = ex.Message };
                        message = JsonConvert.SerializeObject(error, Formatting.Indented);
                        logger?.LogWarning(message);
                    }
                    else
                    {
                        logger?.LogError(ex, "Ocorreu um erro inesperado na chamada da API.");
                        var error = new { context.Response.StatusCode, Mensagem = ex.Message };
                        message = JsonConvert.SerializeObject(error, Formatting.Indented);
                    }

                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync(message);
                });
            });

            app.UseHealthChecks("/health")
                .UseSwagger()
                .UseStaticFiles()
                .UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void ConfigurarLog(IServiceCollection services)
        {
            var env = Configuration.GetValue<string>("ENVIRONMENT");

            var kibanaOptions = new ConfigureLogsOptions()
            {
                Application = "gtw-gestortarifas-api",
                SentryDsn = "",
                ElasticUri = GlobalSecrets.ElasticSearchUrl,
                ElasticSearchIndex = "gtw-gestortarifas-api",
                LogEventLevel = LogEventLevel.Information
            };

            services.ConfigureLogManager(kibanaOptions);
        }

        private void AddCognitoAuthentication(IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(Chave.TOKEN_6_DIGITOS_DIGITAL, options =>
                {
                    options.Authority = GlobalSecrets.ChavesGtwApiGestorTarifas.Cognito6DigitosURL;
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        ValidateLifetime = true,
                        RequireAudience = false,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.FromMinutes(5),
                        // Ensure the token hasn't expired
                        RequireExpirationTime = true,
                        ValidIssuer = GlobalSecrets.ChavesGtwApiGestorTarifas.Cognito6DigitosURL
                    };
                })
                .AddJwtBearer(Chave.TOKEN_4_DIGITOS_DIGITAL, options =>
                {
                    options.Authority = GlobalSecrets.ChavesGtwApiGestorTarifas.Cognito4DigitosURL;
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireAudience = false,
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ClockSkew = TimeSpan.FromMinutes(5),
                        // Ensure the token hasn't expired
                        RequireExpirationTime = true,
                        ValidIssuer = GlobalSecrets.ChavesGtwApiGestorTarifas.Cognito4DigitosURL
                    };
                });
        }
    }
}