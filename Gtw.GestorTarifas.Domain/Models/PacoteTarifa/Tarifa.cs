using System;

namespace Gtw.GestorTarifas.Domain.Models.PacoteTarifa
{
    public class Tarifa
    {
        public int IdTarifaBase { get; set; }
        public string DsDescricao { get; set; }
        public bool FlGerencial { get; set; }
        public int FlReceberDe { get; set; }
        public int FlPagarPara { get; set; }
        public int FlTipoValorTarifa { get; set; }
        public int FlTipoPessoaPermitido { get; set; }
        public int FlTipoCobranca { get; set; }
        public FaixaValorPermitido FaixaValorPermitido { get; set; }
        public FaixaPorcentagemDescontoPermitido FaixaPercentagemDescontoPermitido { get; set; }
        public float VlCusto { get; set; }
        public float VlTarifa { get; set; }
        public float TxPercTarifa { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}