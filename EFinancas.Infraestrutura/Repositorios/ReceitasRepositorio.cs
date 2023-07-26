using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class ReceitasRepositorio : RepositorioBase<Receita>, IReceitasRepositorio
    {
        public ReceitasRepositorio(IMongoDatabase database) : base(database, Collections.Receitas)
        {
        }
    }
}
