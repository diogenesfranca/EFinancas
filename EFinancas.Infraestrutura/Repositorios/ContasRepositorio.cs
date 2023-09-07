using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Infraestrutura.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class ContasRepositorio : RepositorioBase<Conta>, IContasRepositorio
    {
        public ContasRepositorio(IMongoDatabase database) : base(database, Collections.Contas)
        {
        }
    }
}
