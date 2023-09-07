using EFinancas.Dominio.Interfaces.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Infraestrutura.Repositorios
{
    [ExcludeFromCodeCoverage]
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
