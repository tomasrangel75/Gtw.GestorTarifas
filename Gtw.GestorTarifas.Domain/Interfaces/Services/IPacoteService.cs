using Gtw.GestorTarifas.Domain.Dtos;
using Gtw.GestorTarifas.Domain.Dtos.PacoteDto;
using Gtw.GestorTarifas.Domain.Dtos.TrocaPacote;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gtw.GestorTarifas.Domain.Interfaces.Services
{
    public interface IPacoteService
    {
        Task<ResponseGenerico<PacoteResponse>> ObterPacoteAtualAsync(string cpfCnpj);
        Task<ResponseGenerico<List<PacoteResponse>>> ObterPacotesAsync();
        Task<ResponseGenerico<TrocaPacoteClienteResponse>> TrocarPacoteAsync(string cpfCnpj, int idPacoteNovoAtivar);
    }
}