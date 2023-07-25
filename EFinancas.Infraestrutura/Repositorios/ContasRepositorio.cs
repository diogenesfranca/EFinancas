using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class ContasRepositorio : IContasRepositorio
    {
        private readonly IMongoCollection<Conta> collection;

        public ContasRepositorio(IMongoDatabase database)
        {
            collection = database.GetCollection<Conta>(Collections.Contas);
        }

        public async Task<IEnumerable<Conta>> Listar()
        {
            return await collection.Find(Builders<Conta>.Filter.Empty).ToListAsync();
        }

        public Task Inserir(Conta conta)
        {
            return collection.InsertOneAsync(conta);
        }

        public Task Atualizar(Conta conta)
        {
            return collection.ReplaceOneAsync(x => x.Id == conta.Id, conta);
        }

        public Task Deletar(string id)
        {
            return collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
