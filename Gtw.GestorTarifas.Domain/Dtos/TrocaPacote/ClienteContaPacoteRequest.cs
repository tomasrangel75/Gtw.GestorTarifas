using Newtonsoft.Json;
using System;

namespace Gtw.GestorTarifas.Domain.Dtos.TrocaPacote
{
    public class ClienteContaPacoteRequest
    {
        public int IdPacote { get; set; }

        [JsonProperty("DtVigenciaDe")]
        public DateTime DataVigenciaDe { get; set; }

        [JsonProperty("DtVigenciaAte")]
        public DateTime? DataVigenciaAte { get; set; }

        [JsonProperty("FlAcrescimo")]
        public int FlagAcrescimo { get; set; }

        [JsonProperty("FlPorcentagemValor")]
        public int FlagPorcentagemValor { get; set; }

        [JsonProperty("VlDesconto")]
        public int ValorDesconto { get; set; }

        [JsonProperty("TxDesconto")]
        public int TaxaDesconto { get; set; }

        [JsonProperty("NrCarencia")]
        public int NumeroCarencia { get; set; }
    }
}