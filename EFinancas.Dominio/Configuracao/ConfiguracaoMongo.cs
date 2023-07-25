using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Linq.Expressions;
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

                var database = client!.GetDatabase("efinancas");

                CriarIndice<Categoria>(database, Collections.Categorias, true, x => x.Descricao);
                CriarIndice<Conta>(database, Collections.Contas, true, x => x.Descricao);

                return database;
            });
        }

        private static void CriarIndice<TDocument>(IMongoDatabase database, string nomeColecao, bool unico, Expression<Func<TDocument, object>> campo)
        {
            var collection = database.GetCollection<TDocument>(nomeColecao);

            var indexKeysDefinition = Builders<TDocument>.IndexKeys.Ascending(campo);

            collection.Indexes.CreateOne(new CreateIndexModel<TDocument>(indexKeysDefinition, new CreateIndexOptions { Unique = unico }));
        }
    }
}
