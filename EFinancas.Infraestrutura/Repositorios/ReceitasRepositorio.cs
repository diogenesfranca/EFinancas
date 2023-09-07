using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Infraestrutura.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class ReceitasRepositorio : FinancasRepositorio<Receita>, IReceitasRepositorio
    {
        public ReceitasRepositorio(IMongoDatabase database) : base(database, Collections.Receitas)
        {
        }
    }
}
