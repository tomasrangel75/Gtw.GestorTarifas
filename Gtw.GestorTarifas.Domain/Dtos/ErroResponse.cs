using System.Net;
using System.Text.Json.Serialization;

namespace Gtw.GestorTarifas.Domain.Dtos
{
    public class ErroResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
    }
}