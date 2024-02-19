using System;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    public class ClienteConta
    {
        public int IdClienteConta { get; set; }
        public int IdCliente { get; set; }
        public int IdParceiro { get; set; }
        public int IdAgencia { get; set; }
        public int NrAgencia { get; set; }
        public int IdModalidade { get; set; }
        public string CdModalidade { get; set; }
        public int IdGerente { get; set; }
        public int CdGerente { get; set; }
        public bool FlVerificarSaldo { get; set; }
        public float VlRecorrente { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}