using System;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    public class GerenteConta
    {
        public int IdGerenteConta { get; set; }
        public int CdGerente { get; set; }
        public string DsGerente { get; set; }
        public long CdCpf { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}