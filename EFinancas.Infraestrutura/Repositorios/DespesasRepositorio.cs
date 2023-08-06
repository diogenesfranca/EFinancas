using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class DespesasRepositorio : FinancasRepositorio<Despesa>, IDespesasRepositorio
    {
        public DespesasRepositorio(IMongoDatabase database) : base(database, Collections.Despesas)
        {            
        }
    }
}
