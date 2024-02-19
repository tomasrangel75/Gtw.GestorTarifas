using System;

namespace Gtw.GestorTarifas.Domain.Models.PacoteInfo
{
    public class Canal
    {
        public int IdCanal { get; private set; }
        public string DsDescricao { get; private set; }
        public bool FlAtivo { get; private set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}