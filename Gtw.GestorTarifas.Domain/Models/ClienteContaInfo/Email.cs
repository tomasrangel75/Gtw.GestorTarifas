using Newtonsoft.Json;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    public class Email
    {
        [JsonProperty("DsEmail")]
        public string Endereco { get; set; }
    }
}