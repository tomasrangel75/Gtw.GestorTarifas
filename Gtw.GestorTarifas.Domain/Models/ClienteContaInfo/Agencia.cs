using Newtonsoft.Json;
using System;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    public class Agencia
    {
        [JsonProperty("IdAgencia")]
        public int Id { get; set; }

        [JsonProperty("NrAgencia")]
        public int Numero { get; set; }

        [JsonProperty("DsAgencia")]
        public string Nome { get; set; }

        public string DsObs { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}