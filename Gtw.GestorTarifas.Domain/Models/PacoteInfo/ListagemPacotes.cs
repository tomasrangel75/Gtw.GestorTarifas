using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gtw.GestorTarifas.Domain.Models.PacoteInfo
{
    public class ListagemPacotes
    {
        public int TotalRegistros { get; set; }

        [JsonPropertyName("PacoteList")]
        public List<InfoPacote> Pacotes { get; set; }
    }
}