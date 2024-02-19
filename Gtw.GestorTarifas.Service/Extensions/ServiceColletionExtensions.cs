using Gtw.GestorTarifas.Data.Rest;
using Gtw.GestorTarifas.Domain.Aws;
using Gtw.GestorTarifas.Domain.Interfaces.Rest;
using Gtw.GestorTarifas.Domain.Interfaces.Services;
using Gtw.GestorTarifas.Domain.Profiles;
using Gtw.GestorTarifas.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gtw.GestorTarifas.Service.Extensions
{
    public static class ServiceColletionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Services
            services.AddSingleton<IPacoteService, PacoteService>();

            // Repository
            services.AddSingleton<IGestorTarifasApi, GestorTarifasApiRest>();

            // Auto Mapper
            services.AddAutoMapper(typeof(TokenAutenticacaoProfile).Assembly);

            // HTTP
            services.AddHttpClient("api-gestor-tarifas", client =>
            {
                client.BaseAddress = new Uri(GlobalSecrets.WiseConsActions.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(20);
            });

            // Configurações
            services.AddSingleton(GlobalSecrets.ChavesGtwApiGestorTarifas);

            services.AddHealthChecks();

            return services;
        }
    }
}