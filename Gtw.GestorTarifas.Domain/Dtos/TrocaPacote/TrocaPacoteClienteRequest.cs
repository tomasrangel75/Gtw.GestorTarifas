namespace Gtw.GestorTarifas.Domain.Dtos.TrocaPacote
{
    public class TrocaPacoteClienteRequest
    {
        public int IdClienteContaPacoteDesativacao { get; set; }
        public int IdPacoteDesativacao { get; set; }
        public ClienteContaPacoteAtivaDesativaRequest PacoteDesativacao { get; set; } 
        public int IdClienteConta { get; set; }
        public ClienteContaPacoteRequest NovoPacote { get; set; }
    }
}