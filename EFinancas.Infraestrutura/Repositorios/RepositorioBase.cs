using EFinancas.Dominio.Interfaces.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Infraestrutura.Repositorios
{
    [ExcludeFromCodeCoverage]
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T: IIdentificador
    {
        protected readonly IMongoCollection<T> collection;

        public RepositorioBase(IMongoDatabase database, string nomeColecao)
        {
            collection = database.GetCollection<T>(nomeColecao);
        }

        public async Task<T> Obter(string id)
        {
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> Listar()
        {
            return await collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public Task Inserir(T entidade)
        {
            return collection.InsertOneAsync(entidade);
        }

        public Task Atualizar(T entidade)
        {
            return collection.ReplaceOneAsync(x => x.Id == entidade.Id, entidade);
        }

        public Task Deletar(string id)
        {
            return collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
