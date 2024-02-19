using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gtw.GestorTarifas.Domain.Dtos.ClienteDto
{
    public class ClienteContaResponse
    {
        public int TotalRegistros { get; set; }

        [JsonProperty("ClienteContaList")]
        public List<ClienteConta> ClienteContaLista { get; set; } = new List<ClienteConta>();
    }
}