using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class CategoriasRepositorio : ICategoriasRepositorio
    {
        private readonly IMongoCollection<Categoria> collection;

        public CategoriasRepositorio(IMongoDatabase database)
        {
            collection = database.GetCollection<Categoria>(Tabelas.Categorias);
        }

        public async Task<IEnumerable<Categoria>> Listar()
        {
             return await collection.Find(null).ToListAsync();
        }

        public Task Inserir(Categoria categoria)
        {
            return collection.InsertOneAsync(categoria);
        }

        public Task Deletar(string categoria)
        {
            return collection.DeleteManyAsync(x => x.Descricao == categoria);
        }
    }
}
