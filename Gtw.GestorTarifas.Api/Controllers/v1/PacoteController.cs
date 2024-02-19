using Gtw.GestorTarifas.Domain.Constantes;
using Gtw.GestorTarifas.Domain.Dtos.PacoteDto;
using Gtw.GestorTarifas.Domain.Dtos.TrocaPacote;
using Gtw.GestorTarifas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace Gtw.GestorTarifas.Api.Controllers.v1
{
    [Route("gtw-gestortarifas-api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = Chave.TOKEN_6_DIGITOS_DIGITAL)]
    [Authorize(AuthenticationSchemes = Chave.TOKEN_4_DIGITOS_DIGITAL)]
    public class PacoteController : ControllerBase
    {
        private readonly ILogger<PacoteController> _logger;
        private readonly IPacoteService _pacoteService;

        public PacoteController(ILogger<PacoteController> logger, IPacoteService pacoteService)
        {
            _logger = logger;
            _pacoteService = pacoteService;
        }

        [HttpGet("pacote-atual-cliente")]
        [ProducesResponseType(typeof(PacoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PacoteAtualCliente()
        {
            var cpfCnpj = User.FindFirst("cognito:username")?.Value;

            _logger.LogInformation("Operação => Chamada Rota Buscar Pacote Atual Cliente - CpfCpnj: {cpfCpnj}", cpfCnpj);

            var response = await _pacoteService.ObterPacoteAtualAsync(cpfCnpj);

            if (response.CodigoHttp == HttpStatusCode.OK)
            {
                return Ok(response.DadosRetorno);
            }
            else
            {
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
            }
        }

        [HttpGet("listagem-pacotes-app")]
        [ProducesResponseType(typeof(List<PacoteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PacotesDisponiveisApp()
        {
            _logger.LogInformation("Operação => Chamada Rota Listagem Pacotes App");

            var response = await _pacoteService.ObterPacotesAsync();

            if (response.CodigoHttp == HttpStatusCode.OK)
            {
                return Ok(response.DadosRetorno);
            }
            else
            {
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
            }
        }

        [HttpPut("mudanca-pacote-cliente")]
        [ProducesResponseType(typeof(TrocaPacoteClienteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([Required] int idPacoteNovoAtivar)
        {
            var cpfCnpj = User.FindFirst("cognito:username")?.Value;

            _logger.LogInformation("Operação => Chamada Rota Mudança de Pacote Cliente - CpfCpnj: {cpfCpnj} | IdPacoteNovoAtivar {idPacoteNovoAtivar}", cpfCnpj, idPacoteNovoAtivar);

            var response = await _pacoteService.TrocarPacoteAsync(cpfCnpj, idPacoteNovoAtivar);

            if (response.CodigoHttp == HttpStatusCode.OK)
            {
                return Ok(response.DadosRetorno);
            }
            else
            {
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
            }
        }
    }
}