using AutoMapper;
using Gtw.GestorTarifas.Domain.Dtos;
using Gtw.GestorTarifas.Domain.Dtos.PacoteDto;
using Gtw.GestorTarifas.Domain.Dtos.ClienteDto;
using Gtw.GestorTarifas.Domain.Dtos.TrocaPacote;
using Gtw.GestorTarifas.Domain.Dtos.SegurancaToken;
using Gtw.GestorTarifas.Domain.Interfaces.Rest;
using Gtw.GestorTarifas.Domain.Models.Configuracao;
using Gtw.GestorTarifas.Domain.Models.PacoteInfo;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gtw.GestorTarifas.Data.Rest
{
    public class GestorTarifasApiRest : IGestorTarifasApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<GestorTarifasApiRest> _logger;
        private readonly IMapper _mapper;
        private readonly ChavesGtwApiGestorTarifas _chavesGtwApiGestorTarifas;

        public GestorTarifasApiRest(
            IHttpClientFactory httpClientFactory,
            ILogger<GestorTarifasApiRest> logger,
            IMapper mapper,
            ChavesGtwApiGestorTarifas chavesGtwApiGestorTarifas
        )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _mapper = mapper;
            _chavesGtwApiGestorTarifas = chavesGtwApiGestorTarifas;
        }

        public async Task<ResponseGenerico<ListagemPacotes>> BuscarPacoteAtualAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Pacote?FlAtivo=true");
            var response = new ResponseGenerico<ListagemPacotes>();

            _logger.LogInformation("Operação => Buscar Pacote Atual | GET Request: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Buscar Pacote Atual | SUCESSO | GET Response: {responseBody}", conteudoResponse);

                    var objetoResponse = System.Text.Json.JsonSerializer.Deserialize<ListagemPacotes>(conteudoResponse);
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    response.DadosRetorno = objetoResponse;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Buscar Pacote Atual | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        public async Task<ResponseGenerico<ListagemPacotes>> BuscarPacotesAtivosAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Pacote?FlAtivo=true");
            var response = new ResponseGenerico<ListagemPacotes>();

            _logger.LogInformation("Operação => Buscar Pacotes Ativos | GET Request: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Buscar Pacotes Ativos | SUCESSO | GET Response: {responseBody}", conteudoResponse);
                    
                    var objetoResponse = System.Text.Json.JsonSerializer.Deserialize<ListagemPacotes>(conteudoResponse);
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    response.DadosRetorno = objetoResponse;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Buscar Pacotes Ativos | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        public async Task<ResponseGenerico<PacoteValorResponse>> BuscarValorPacote(int idPacote)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/PacoteValor/{idPacote}");
            var response = new ResponseGenerico<PacoteValorResponse>();

            _logger.LogInformation("Operação => Buscar Valor Pacote | GET Request: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Buscar Valor Pacote | SUCESSO | GET Response: {responseBody}", conteudoResponse);

                    var objetoResponse = System.Text.Json.JsonSerializer.Deserialize<PacoteValorResponse>(conteudoResponse);

                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;
                    response.DadosRetorno = objetoResponse;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Buscar Valor Pacote | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        public async Task<ResponseGenerico<PacoteTarifaResponse>> BuscarTarifasPacote(int idPacote)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/PacoteTarifa/{idPacote}");
            var response = new ResponseGenerico<PacoteTarifaResponse>();

            _logger.LogInformation("Operação => Buscar Pacote Tarifas | GET Request: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Buscar Pacote Tarifas | SUCESSO | GET Response: {responseBody}", conteudoResponse);

                    var objetoResponse = System.Text.Json.JsonSerializer.Deserialize<PacoteTarifaResponse>(conteudoResponse);

                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;
                    response.DadosRetorno = objetoResponse;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Buscar Pacote Tarifas | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        public async Task<ResponseGenerico<ClienteResponse>> BuscarClienteAsync(ClienteRequest clienteRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Cliente?CdCnpjCpf={clienteRequest.CnpjCpf}");
            var response = new ResponseGenerico<ClienteResponse>();

            _logger.LogInformation("Operação => Buscar Cliente | GET Request: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Buscar Cliente | SUCESSO | GET Response: {responseBody}", conteudoResponse);
                    
                    var objetoResponse = JsonConvert.DeserializeObject<ClienteResponse>(conteudoResponse);

                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;
                    response.DadosRetorno = objetoResponse;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Buscar Cliente | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        public async Task<ResponseGenerico<ClienteContaResponse>> BuscarClienteContaAsync(int idCliente)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/ClienteConta/{idCliente}");
            var response = new ResponseGenerico<ClienteContaResponse>();

            _logger.LogInformation("Operação => Buscar Cliente Conta | GET {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Buscar Cliente Conta | SUCESSO | GET Response: {responseBody}", conteudoResponse);
                    
                    var objetoResponse = JsonConvert.DeserializeObject<ClienteContaResponse>(conteudoResponse);

                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;
                    response.DadosRetorno = objetoResponse;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Buscar Cliente Conta | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        public async Task<ResponseGenerico<ClienteContaPacoteBuscarResponse>> BuscarClienteContaPacoteAsync(int idClienteConta)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/ClienteContaPacote/{idClienteConta}");
            var response = new ResponseGenerico<ClienteContaPacoteBuscarResponse>();

            _logger.LogInformation("Operação => Buscar Cliente Conta Pacote | GET Request: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Buscar Cliente Conta Pacote | SUCESSO | GET Response: {responseBody}", conteudoResponse);
                    
                    var objetoResponse = JsonConvert.DeserializeObject<ClienteContaPacoteBuscarResponse>(conteudoResponse);

                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;
                    response.DadosRetorno = objetoResponse;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Buscar Cliente Conta Pacote | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }
        
        public async Task<ResponseGenerico<TrocaPacoteClienteResponse>> TrocarPacoteClienteAsync(TrocaPacoteClienteRequest request)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var response = new ResponseGenerico<TrocaPacoteClienteResponse>();
            var objetoResponse = new TrocaPacoteClienteResponse();

            _logger.LogInformation("Operação => Início Mudança de Pacote Cliente");

            var desativaPacoteClienteResponse = await AtivarDesativarPacoteCliente(request.IdClienteContaPacoteDesativacao, request.PacoteDesativacao);
            if (desativaPacoteClienteResponse.CodigoHttp != HttpStatusCode.OK)
            {
                response.CodigoHttp = desativaPacoteClienteResponse.CodigoHttp;

                var mensagem = desativaPacoteClienteResponse.ErroRetorno.Mensagem;
                var conteudoErro = $"Erro ao Desativar Pacote Cliente => IdClienteContaPacote: {desativaPacoteClienteResponse.DadosRetorno.IdClienteContaPacote} | IdPacote: {request.IdPacoteDesativacao} - Retorno API Gestor Tarifas JSON: {mensagem}";

                desativaPacoteClienteResponse.ErroRetorno.Mensagem = conteudoErro;
                response.ErroRetorno = desativaPacoteClienteResponse.ErroRetorno;

                _logger.LogError("Operação => Ativar Desativar Pacote Cliente | ERRO | PUT Response: {responseBody}", conteudoErro);

                return response;
            }

            var pacoteNovoIncluidoCliente = await IncluirNovoPacoteClienteAsync(request.IdClienteConta, request.NovoPacote);
            if (pacoteNovoIncluidoCliente.CodigoHttp != HttpStatusCode.OK)
            {
                response.CodigoHttp = pacoteNovoIncluidoCliente.CodigoHttp;

                var mensagem = pacoteNovoIncluidoCliente.ErroRetorno.Mensagem;
                var conteudoErro = $"Erro ao Incluir Pacote Cliente => IdPacote: {request.NovoPacote.IdPacote} - Retorno API Gestor Tarifas JSON: {mensagem}";

                pacoteNovoIncluidoCliente.ErroRetorno.Mensagem = conteudoErro;
                response.ErroRetorno = pacoteNovoIncluidoCliente.ErroRetorno;

                _logger.LogError("Operação => Incluir Pacote Cliente | ERRO | POST Response: {responseBody}", conteudoErro);

                return response;
            }

            objetoResponse.IdPacoteAnteriorDesativado = request.IdPacoteDesativacao;
            objetoResponse.IdNovoPacoteAtivado = request.NovoPacote.IdPacote;
            objetoResponse.MensagemOperacao = "Alteração de Pacote realizada com Sucesso! " +
                $"Pacote Anterior => IdClienteContaPacote: {desativaPacoteClienteResponse.DadosRetorno.IdClienteContaPacote} | IdPacote: {request.IdPacoteDesativacao} - " +
                $"Pacote Atual => IdClienteContaPacote: {pacoteNovoIncluidoCliente.DadosRetorno.IdClienteContaPacote} | IdPacote: {request.NovoPacote.IdPacote}";

            response.CodigoHttp = HttpStatusCode.OK;
            response.DadosRetorno = objetoResponse;

            var retornoLog = System.Text.Json.JsonSerializer.Serialize(objetoResponse);
            _logger.LogInformation("Operação => Fim Mudança de Pacote Cliente | SUCESSO | POST Response: {responseBody}", retornoLog);

            return response;
        }

        private async Task<ResponseGenerico<ClienteContaPacoteAtivaDesativaResponse>> AtivarDesativarPacoteCliente(int idClienteContaPacote, ClienteContaPacoteAtivaDesativaRequest ativaDesativaPacoteRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/ClienteContaPacote/{idClienteContaPacote}/AtivarDesativar");
            var response = new ResponseGenerico<ClienteContaPacoteAtivaDesativaResponse>();

            _logger.LogInformation("Operação => Ativar Desativar Pacote Cliente | PUT RequestUri: {_requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                var conteudoRequest = new StringContent(JsonConvert.SerializeObject(ativaDesativaPacoteRequest), Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                request.Content = conteudoRequest;

                var requestBodyLog = System.Text.Json.JsonSerializer.Serialize(ativaDesativaPacoteRequest);
                _logger.LogInformation("Operação => Ativar Desativar Pacote Cliente | PUT RequestBody: {requestBody}", requestBodyLog);

                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Ativar Desativar Pacote Cliente | SUCESSO | PUT Response: {responseBody}", conteudoResponse);
                    
                    var objetoResponse = JsonConvert.DeserializeObject<ClienteContaPacoteAtivaDesativaResponse>(conteudoResponse);
                    response.DadosRetorno = objetoResponse;
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Ativar Desativar Pacote Cliente | ERRO | PUT Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        private async Task<ResponseGenerico<ClienteContaPacoteIncluirResponse>> IncluirNovoPacoteClienteAsync(int idClienteConta, ClienteContaPacoteRequest clienteContaPacoteRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/ClienteContaPacote/{idClienteConta}");
            var response = new ResponseGenerico<ClienteContaPacoteIncluirResponse>();

            _logger.LogInformation("Operação => Incluir Novo Pacote Cliente | POST RequestUri: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var token = await BuscarTokenAsync();

                var conteudoRequest = new StringContent(JsonConvert.SerializeObject(clienteContaPacoteRequest), Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.Token);
                request.Content = conteudoRequest;

                var requestBodyLog = System.Text.Json.JsonSerializer.Serialize(clienteContaPacoteRequest);
                _logger.LogInformation("Operação => Incluir Novo Pacote Cliente | POST RequestBody: {requestBody}", requestBodyLog);

                var responseGestorTarifasApi = await client.SendAsync(request);
                var conteudoResponse = await responseGestorTarifasApi.Content.ReadAsStringAsync();

                if (responseGestorTarifasApi.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Operação => Incluir Novo Pacote Cliente | SUCESSO | POST Response: {responseBody}", conteudoResponse);
                    
                    var objetoResponse = JsonConvert.DeserializeObject<ClienteContaPacoteIncluirResponse>(conteudoResponse);
                    response.DadosRetorno = objetoResponse;
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;
                }
                else
                {
                    response.CodigoHttp = responseGestorTarifasApi.StatusCode;

                    var erroResponse = new ErroResponse
                    {
                        StatusCode = response.CodigoHttp,
                        Mensagem = conteudoResponse
                    };

                    var conteudo = System.Text.Json.JsonSerializer.Serialize(erroResponse);
                    response.ErroRetorno = System.Text.Json.JsonSerializer.Deserialize<ErroResponse>(conteudo);

                    _logger.LogError("Operação => Incluir Novo Pacote Cliente | ERRO | POST Response: {responseBody}", conteudo);
                }
            }

            return response;
        }

        private async Task<TokenResponse> BuscarTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/seg/SegurancaTokenObterToken");

            _logger.LogInformation("Operação => Requisicao Token de Autenticação API Gestor Tarifas | POST RequestUri: {requestUri}", request.RequestUri);

            using (var client = _httpClientFactory.CreateClient("api-gestor-tarifas"))
            {
                var tokenRequest = _mapper.Map<TokenRequest>(_chavesGtwApiGestorTarifas);
                var serializeUsuario = System.Text.Json.JsonSerializer.Serialize(tokenRequest);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_chavesGtwApiGestorTarifas.TipoAutenticacao, _chavesGtwApiGestorTarifas.TokenAutenticacao);
                request.Content = new StringContent(serializeUsuario, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var contentResp = await response.Content.ReadAsStringAsync();

                    var objResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(contentResp);

                    _logger.LogInformation("Operação => Requisicao Token de Autenticação API Gestor Tarifas | SUCESSO");

                    return objResponse;
                }
                else
                {
                    var contentResp = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Operação => Requisicao Token de Autenticação API Gestor Tarifas | ERRO | POST Response: {responseBody}", contentResp);
                    return new TokenResponse { TokenType = "Bearer" };
                }
            }
        }
    }
}