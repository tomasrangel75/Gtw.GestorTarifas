using Gtw.GestorTarifas.Domain.Models.Configuracao.Tarifa;

namespace Gtw.GestorTarifas.Domain.Models.Configuracao
{
    public class ConfiguracaoNegocio
    {
        public string[] PacotesAPP { get; set; }
        public Tarifas Tarifas { get; set; }
    }
}