using Gtw.GestorTarifas.Domain.Enums;
using System;

namespace Gtw.GestorTarifas.Domain.Models.PacoteInfo
{
    public class EventoCobranca
    {
        public int IdTipoEvento { get; set; }
        public int CdEvento { get; set; }
        public string DsEvento { get; set; }
        public int IdCanal { get; set; }
        public Canal Canal { get; set; }
        public TipoEventoEnum FlTipoEvento { get; set; }
        public TipoPessoaPermitidoEnum FlTipoPessoaPermitido { get; set; }
        public PeriodicidadeEnum FlPeriodicidadeLiq { get; set; }
        public bool FlBuscarCC { get; set; }
        public string CdHistoricoCC { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}