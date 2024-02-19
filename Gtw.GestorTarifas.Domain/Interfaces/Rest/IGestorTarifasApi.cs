using Gtw.GestorTarifas.Domain.Dtos;
using Gtw.GestorTarifas.Domain.Dtos.PacoteDto;
using Gtw.GestorTarifas.Domain.Dtos.ClienteDto;
using Gtw.GestorTarifas.Domain.Dtos.TrocaPacote;
using Gtw.GestorTarifas.Domain.Models.PacoteInfo;
using System.Threading.Tasks;

namespace Gtw.GestorTarifas.Domain.Interfaces.Rest
{
    public interface IGestorTarifasApi
    {
        Task<ResponseGenerico<ListagemPacotes>> BuscarPacotesAtivosAsync();
        Task<ResponseGenerico<PacoteValorResponse>> BuscarValorPacote(int idPacote);
        Task<ResponseGenerico<PacoteTarifaResponse>> BuscarTarifasPacote(int idPacote);
        Task<ResponseGenerico<ClienteContaPacoteBuscarResponse>> BuscarClienteContaPacoteAsync(int idClienteConta);
        Task<ResponseGenerico<ClienteResponse>> BuscarClienteAsync(ClienteRequest clienteRequest);
        Task<ResponseGenerico<ClienteContaResponse>> BuscarClienteContaAsync(int idCliente);
        Task<ResponseGenerico<TrocaPacoteClienteResponse>> TrocarPacoteClienteAsync(TrocaPacoteClienteRequest trocaPacoteClienteRequest);
    }
}