using Newtonsoft.Json;

namespace Gtw.GestorTarifas.Domain.Dtos.ClienteDto
{
    public class ClienteResponse
    {
        public int TotalRegistros { get; set; }

        [JsonProperty("ClienteList")]
        public Cliente[] Clientes { get; set; }
    }
}