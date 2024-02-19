namespace Gtw.GestorTarifas.Domain.Dtos.TrocaPacote
{
    public class TrocaPacoteClienteResponse
    {
        public int IdPacoteAnteriorDesativado { get; set; }
        public int IdNovoPacoteAtivado { get; set; }
        public string MensagemOperacao { get; set; }
    }
}
