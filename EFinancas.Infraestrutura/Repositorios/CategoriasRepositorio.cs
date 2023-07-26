using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class CategoriasRepositorio : RepositorioBase<Categoria>, ICategoriasRepositorio
    {
        public CategoriasRepositorio(IMongoDatabase database) : base(database, Collections.Categorias)
        {
        }
    }
}
