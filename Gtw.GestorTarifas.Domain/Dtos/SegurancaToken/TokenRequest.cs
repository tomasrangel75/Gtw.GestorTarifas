using System.Text.Json.Serialization;

namespace Gtw.GestorTarifas.Domain.Dtos.SegurancaToken
{
    public class TokenRequest
    {
        [JsonPropertyName("Usuario")]
        public string UsuarioAutenticacao { get; set; }
        
        [JsonPropertyName("Senha")]
        public string SenhaAutenticacao { get; set; }
        
        [JsonPropertyName("Module")]
        public string ModuleAutenticacao { get; set; }
    }
}