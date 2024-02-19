namespace Gtw.GestorTarifas.Domain.Dtos.PacoteDto
{
    public class PacoteResponse
    {
        public int IdPacote { get; set; }
        public string NomePacote { get; set; }
        public double ValorMensalPacote { get; set; }
        public string Extrato { get; set; }
        public string ManutencaoContaPagamento { get; set; }
        public string TransferenciaExterna { get; set; }
        public string TransferenciaInterna { get; set; }
        public string EmissaoBoleto { get; set; }
        public string ComunicacaoDigitalAvisoCelular { get; set; }
        public string Email { get; set; }
    }
}