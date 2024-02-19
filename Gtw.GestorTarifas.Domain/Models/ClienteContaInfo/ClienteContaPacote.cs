using Gtw.GestorTarifas.Domain.Models.PacoteInfo;
using System;

namespace Gtw.GestorTarifas.Domain.Models.ClienteContaInfo
{
    public class ClienteContaPacote
    {
        public int IdClienteContaPacote { get; set; }
        public int IdPacote { get; set; }
        public InfoPacote Pacote { get; set; }
        public int IdClienteConta { get; set; }
        public ClienteConta ClienteConta { get; set; }
        public Vigencia Vigencia { get; set; }
        public int FlAcrescimo { get; set; }
        public int FlPorcentagemValor { get; set; }
        public double VlDesconto { get; set; }
        public double TxDesconto { get; set; }
        public int NrCarencia { get; set; }
        public DateTime? DtPrxRenovacao { get; set; }
        public DateTime? DtUltCobranca { get; set; }
        public object VlUltCobranca { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime? DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime? DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
        public int? IdProcessoBacklog { get; set; }
        public object ProcessoBacklog { get; set; }
    }
}