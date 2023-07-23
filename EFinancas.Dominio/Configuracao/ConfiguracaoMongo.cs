using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Security.Authentication;

namespace EFinancas.Dominio.Configuracao
{
    public static class ConfiguracaoMongo
    {
        public static void ConfigurarMongo(this IServiceCollection services)
        {
            var settings = new MongoClientSettings
            {
                UseTls = true,
                SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 },
            };

            services.AddSingleton<IMongoClient>(new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")));
            
            services.AddSingleton(sp =>
            {
                var client = sp.GetService<IMongoClient>();                
                return client.GetDatabase("efinancas");
            });            
        }
    }
}
