using Amazon.DynamoDBv2.DataModel;

namespace Gtw.GestorTarifas.Domain.Models.Configuracao
{
    [DynamoDBTable("Master-Configuracoes-Sistema")]
    public class ConfiguracaoApiGtwGestorTarifas
    {
        [DynamoDBProperty("sistema")]
        public string Sistema { get; set; }

        [DynamoDBProperty("ConfigNegocio")]
        public ConfiguracaoNegocio ConfiguracaoNegocio { get; set; }
    }
}