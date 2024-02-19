namespace Gtw.GestorTarifas.Domain.Dtos.SegurancaToken
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public int IdUsuario { get; set; }
        public string CdUsuario { get; set; }
        public string NmUsuario { get; set; }
        public string Email { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}