namespace Gtw.GestorTarifas.Domain.Models.Configuracao
{
    public class ChavesGtwApiGestorTarifas
    {
        public string UsuarioAutenticacao { get; set; }
        public string SenhaAutenticacao { get; set; }
        public string ModuleAutenticacao { get; set; }
        public string TipoAutenticacao { get; set; }
        public string TokenAutenticacao { get; set; }
        public string Cognito6DigitosURL { get; set; }
        public string Cognito4DigitosURL { get; set; }
    }
}