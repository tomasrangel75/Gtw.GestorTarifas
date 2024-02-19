using Newtonsoft.Json;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    [JsonObject("Cnpjcpf")]
    public class CnpjCpf
    {
        [JsonProperty("FlTipoPessoa")]
        public int TipoPessoa { get; set; }

        [JsonProperty("CdCnpjCpf")]
        public long Codigo { get; set; }
    }
}