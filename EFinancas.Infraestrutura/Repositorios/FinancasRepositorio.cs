using EFinancas.Dominio.Interfaces.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public abstract class FinancasRepositorio<T> : RepositorioBase<T>, IFinancasRepositorio<T> where T : IFinanca
    {
        public FinancasRepositorio(IMongoDatabase database, string nomeColecao) : base(database, nomeColecao)
        {
        }

        public async Task<IEnumerable<T>> Listar(string idConta)
        {
            return await collection.Find(x => x.IdConta == idConta).ToListAsync();
        }
    }
}
