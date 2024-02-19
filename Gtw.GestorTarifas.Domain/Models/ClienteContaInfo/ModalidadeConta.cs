using Newtonsoft.Json;
using System;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    public class ModalidadeConta
    {
        [JsonProperty("IdModalidadeConta")]
        public int Id { get; set; }

        [JsonProperty("CdModalidade")]
        public string Codigo { get; set; }

        [JsonProperty("DsModalidade")]
        public string Descricao { get; set; }

        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}