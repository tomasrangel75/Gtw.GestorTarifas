using System;

namespace Gtw.GestorTarifas.Domain.Models.PacoteInfo
{
    public class InfoPacote
    {
        public int IdPacote { get; set; }
        public string DsPacote { get; set; }
        public int FlTipoPessoa { get; set; }
        public int FlPeriodicidade { get; set; }
        public int FlDataCobranca { get; set; }
        public int NrDiaCobranca { get; set; }
        public int IdEventoCobranca { get; set; }
        public EventoCobranca EventoCobranca { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}