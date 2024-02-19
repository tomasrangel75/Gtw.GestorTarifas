using Gtw.GestorTarifas.Domain.Models.ClienteContaInfo;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gtw.GestorTarifas.Domain.Dtos.ClienteDto
{
    public class ClienteContaPacoteBuscarResponse
    {
        public int TotalRegistros { get; set; }
        
        [JsonProperty("ClienteContaPacoteList")]
        public List<ClienteContaPacote> ClienteContaPacoteLista { get; set; }
    }
}