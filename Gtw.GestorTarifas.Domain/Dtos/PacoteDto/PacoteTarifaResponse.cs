using Gtw.GestorTarifas.Domain.Models.PacoteTarifa;
using System.Collections.Generic;

namespace Gtw.GestorTarifas.Domain.Dtos.PacoteDto
{
    public class PacoteTarifaResponse
    {
        public int TotalRegistros { get; set; }
        public List<PacoteTarifa> PacoteTarifaList { get; set; }
    }
}