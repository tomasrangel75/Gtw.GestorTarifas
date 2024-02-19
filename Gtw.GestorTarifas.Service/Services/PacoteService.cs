using Gtw.GestorTarifas.Data.Repositories;
using Gtw.GestorTarifas.Domain.Constantes;
using Gtw.GestorTarifas.Domain.Dtos;
using Gtw.GestorTarifas.Domain.Dtos.PacoteDto;
using Gtw.GestorTarifas.Domain.Dtos.ClienteDto;
using Gtw.GestorTarifas.Domain.Dtos.TrocaPacote;
using Gtw.GestorTarifas.Domain.Enums;
using Gtw.GestorTarifas.Domain.Extensions;
using Gtw.GestorTarifas.Domain.Interfaces.Rest;
using Gtw.GestorTarifas.Domain.Interfaces.Services;
using Gtw.GestorTarifas.Domain.Models.Configuracao;
using Gtw.GestorTarifas.Domain.Models.PacoteInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Gtw.GestorTarifas.Domain.Exceptions;

namespace Gtw.GestorTarifas.Service.Services
{
    public class PacoteService : IPacoteService
    {
        private readonly IGestorTarifasApi _gestorTarifasApi;

        public PacoteService(IGestorTarifasApi gestorTarifasApi)
        {
            _gestorTarifasApi = gestorTarifasApi;
        }

        public async Task<ResponseGenerico<PacoteResponse>> ObterPacoteAtualAsync(string cpfCnpj)
        {
            var response = new ResponseGenerico<PacoteResponse>();

            var cliente = await _gestorTarifasApi.BuscarClienteAsync(new ClienteRequest() { CnpjCpf = cpfCnpj });

            if (cliente.DadosRetorno.TotalRegistros == 0)
            {
                response.CodigoHttp = HttpStatusCode.NoContent;
            }
            else
            {
                // Buscar Cliente Conta | IdClienteConta
                var clienteConta = await _gestorTarifasApi.BuscarClienteContaAsync(cliente.DadosRetorno.Clientes[0].IdCliente);

                if (clienteConta.DadosRetorno.TotalRegistros == 0)
                {
                    throw new MasterException("Conta do Cliente ainda não foi Cadastrada no Tarifador!");
                }

                var idClienteConta = clienteConta.DadosRetorno.ClienteContaLista.Find(m => m.CdModalidade == "00035").IdClienteConta;

                // Buscar Cliente Pacote Atual
                var clienteContaPacote = await _gestorTarifasApi.BuscarClienteContaPacoteAsync(idClienteConta);
                var pacoteAtual = clienteContaPacote.DadosRetorno.ClienteContaPacoteLista.Find(pA => pA.FlAtivo);

                if (pacoteAtual == null)
                {
                    response.CodigoHttp = HttpStatusCode.NoContent;
                }
                else
                {
                    var objetoResponse = await MontarObjetoPacote(pacoteAtual.Pacote);

                    response.CodigoHttp = HttpStatusCode.OK;
                    response.DadosRetorno = objetoResponse;
                }
            }

            return response;
        }

        public async Task<ResponseGenerico<List<PacoteResponse>>> ObterPacotesAsync()
        {
            var pacotes = await _gestorTarifasApi.BuscarPacotesAtivosAsync();
            var idsPacotesAPP = await ObterConfiguracoesApiGtwGestorTarifasAsync();
            
            var pacotesAPP = pacotes.DadosRetorno.Pacotes.Where(p => idsPacotesAPP.ConfiguracaoNegocio.PacotesAPP.Contains(p.IdPacote.ToString())).ToList();

            var response = new ResponseGenerico<List<PacoteResponse>>();            
            var listaPacotes = new List<PacoteResponse>();

            foreach (var pacoteAPP in pacotesAPP)
            {
                var pacote = await MontarObjetoPacote(pacoteAPP);

                listaPacotes.Add(pacote);
            }

            response.CodigoHttp = HttpStatusCode.OK;
            response.DadosRetorno = listaPacotes;

            return response;
        }

        public async Task<ResponseGenerico<TrocaPacoteClienteResponse>> TrocarPacoteAsync(string cpfCnpj, int idPacoteNovoAtivar)
        {
            var clienteTarifa = await _gestorTarifasApi.BuscarClienteAsync(new ClienteRequest() { CnpjCpf = cpfCnpj });

            // Buscar Cliente Conta | IdClienteConta
            var clienteConta = await _gestorTarifasApi.BuscarClienteContaAsync(clienteTarifa.DadosRetorno.Clientes[0].IdCliente);
            var idClienteConta = clienteConta.DadosRetorno.ClienteContaLista.Find(m => m.CdModalidade == "00035").IdClienteConta;
            
            // Desativar Pacote Atual
            var clienteContaPacote = await _gestorTarifasApi.BuscarClienteContaPacoteAsync(idClienteConta);
            var pacoteParaDesativacao = clienteContaPacote.DadosRetorno.ClienteContaPacoteLista.Find(pA => pA.FlAtivo);

            ClienteContaPacoteAtivaDesativaRequest desativaPacoteClienteRequest = new ClienteContaPacoteAtivaDesativaRequest { FlagAtivo = false };

            // Incluir Novo Pacote
            ClienteContaPacoteRequest clienteContaPacoteRequest = new ClienteContaPacoteRequest
            {
                DataVigenciaDe = DateTime.Now,
                FlagAcrescimo = 1,
                FlagPorcentagemValor = 1,
                IdPacote = idPacoteNovoAtivar,
                NumeroCarencia = 0
            };

            // Mudança do Pacote
            TrocaPacoteClienteRequest trocaPacoteClienteRequest = new TrocaPacoteClienteRequest
            {
                IdClienteContaPacoteDesativacao = pacoteParaDesativacao.IdClienteContaPacote,
                IdPacoteDesativacao = pacoteParaDesativacao.Pacote.IdPacote,
                PacoteDesativacao = desativaPacoteClienteRequest,
                IdClienteConta = idClienteConta,
                NovoPacote = clienteContaPacoteRequest
            };

            var ativarDesativarPacote = await _gestorTarifasApi.TrocarPacoteClienteAsync(trocaPacoteClienteRequest);

            return ativarDesativarPacote;
        }

        private async Task<PacoteResponse> MontarObjetoPacote(InfoPacote infoPacote)
        {
            var configApi = await ObterConfiguracoesApiGtwGestorTarifasAsync();

            var pacote = new PacoteResponse();

            var pacoteValor = await _gestorTarifasApi.BuscarValorPacote(infoPacote.IdPacote);
            var tarifasPacote = await _gestorTarifasApi.BuscarTarifasPacote(infoPacote.IdPacote);

            pacote.IdPacote = infoPacote.IdPacote;
            pacote.NomePacote = infoPacote.DsPacote;

            var valorMensalPacote = pacoteValor.DadosRetorno.PacoteValorList.FirstOrDefault(vlp => vlp.FlAtivo && vlp.VigenciaFaixaData.DtAte >= DateTime.Today); // ATIVO E DENTRO DA VIGÊNCIA
            pacote.ValorMensalPacote = (valorMensalPacote == null) ? 0 : valorMensalPacote.VlPacote;
            
            pacote.Extrato = "Ilimitado";

            pacote.ManutencaoContaPagamento = ((PeriodicidadeEnum)infoPacote.FlPeriodicidade).GetDisplayName();

            var ted = tarifasPacote.DadosRetorno.PacoteTarifaList.Find(tar => tar.IdTarifa == configApi.ConfiguracaoNegocio.Tarifas.TransferenciaExterna.Id); // FILTRAR PELO ID CONFIGURADO
            if (ted != null)
            {
                pacote.TransferenciaExterna = ted.Quantidade == 999 ? "Ilimitado" : ted.Quantidade.ToString();
            }

            pacote.TransferenciaInterna = "Ilimitado";
            pacote.EmissaoBoleto = "Ilimitado";
            pacote.ComunicacaoDigitalAvisoCelular = "0";
            pacote.Email = "";

            return pacote;
        }
        
        private async Task<ConfiguracaoApiGtwGestorTarifas> ObterConfiguracoesApiGtwGestorTarifasAsync()
        {
            var configuracaoRepository = new DynamoBaseRepository<ConfiguracaoApiGtwGestorTarifas>();

            return await configuracaoRepository.BuscarAsync(Chave.CHAVE_CONFIGURACAO);
        }
    }
}