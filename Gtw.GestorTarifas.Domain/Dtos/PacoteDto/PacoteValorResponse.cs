using Gtw.GestorTarifas.Domain.Models.PacoteValor;
using System.Collections.Generic;

namespace Gtw.GestorTarifas.Domain.Dtos.PacoteDto
{
    public class PacoteValorResponse
    {
        public int TotalRegistros { get; set; }
        public List<PacoteValor> PacoteValorList { get; set; }
    }
}