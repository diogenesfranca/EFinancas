using EFinancas.Dominio.Interfaces.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T: IIdentificador
    {
        private readonly IMongoCollection<T> collection;

        public RepositorioBase(IMongoDatabase database, string nomeColecao)
        {
            collection = database.GetCollection<T>(nomeColecao);
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
