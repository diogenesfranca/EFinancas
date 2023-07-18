using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;

namespace EFinancas.Api.Configuracao
{
    public static class ConfiguracaoDynamoDB
    {
        public static void ConfigurarDynamo(this IServiceCollection services)
        {
            var credentials = new BasicAWSCredentials(Environment.GetEnvironmentVariable("AWS_DYNAMO_DB_ACCESS_KEY"), Environment.GetEnvironmentVariable("AWS_DYNAMO_DB_SECRET_KEY"));

            var client = new AmazonDynamoDBClient(credentials, new AmazonDynamoDBConfig { RegionEndpoint = RegionEndpoint.USEast1 });

            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        }
    }
}
