using Amazon;
using Amazon.DynamoDBv2;

namespace EFinancas.Api.Configuracoes
{
    public static class ConfiguracaoDynamoDB
    {
        public static void ConfigurarDynamo(this IServiceCollection services)
        {
            var clientConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                
            };

            services.AddScoped(typeof(IAmazonDynamoDB), sp =>
            {
                return new AmazonDynamoDBClient(clientConfig);
            });
        }
    }
}
