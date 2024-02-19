using Newtonsoft.Json;

namespace Gtw.GestorTarifas.Domain.Dtos.TrocaPacote
{
    public class ClienteContaPacoteAtivaDesativaRequest
    {
        [JsonProperty("FlAtivo")]
        public bool FlagAtivo { get; set; }
    }
}