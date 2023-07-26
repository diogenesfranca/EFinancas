using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class ContasRepositorio : RepositorioBase<Conta>, IContasRepositorio
    {
        public ContasRepositorio(IMongoDatabase database) : base(database, Collections.Contas)
        {
        }
    }
}
