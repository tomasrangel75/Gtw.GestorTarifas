using System;

namespace Gtw.GestorTarifas.Domain.Models.PacoteTarifa
{
    public class PacoteTarifa
    {
        public int IdPacoteTarifa { get; set; }
        public int IdPacote { get; set; }
        public object Pacote { get; set; }
        public int IdTarifa { get; set; }
        public Tarifa Tarifa { get; set; }
        public int Quantidade { get; set; }
        public int FlAcrescimo { get; set; }
        public int FlPorcentagemValor { get; set; }
        public float VlDesconto { get; set; }
        public float TxDesconto { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}