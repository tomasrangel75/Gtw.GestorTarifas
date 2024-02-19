using System;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    public class Parceiro
    {
        public int IdParceiro { get; set; }
        public string DsNome { get; set; }
        public long CdCnpj { get; set; }
        public string CdHashLegado { get; set; }
        public float TxComissao { get; set; }
        public float TxComissaoAgente1 { get; set; }
        public float TxComissaoAgente2 { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}