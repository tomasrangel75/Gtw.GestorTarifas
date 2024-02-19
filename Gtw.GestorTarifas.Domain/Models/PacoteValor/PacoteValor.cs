using System;

namespace Gtw.GestorTarifas.Domain.Models.PacoteValor
{
    public class PacoteValor
    {
        public int IdPacoteValor { get; set; }
        public int IdPacote { get; set; }
        public object Pacote { get; set; }
        public VigenciaFaixaData VigenciaFaixaData { get; set; }
        public double VlPacote { get; set; }
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
    }
}