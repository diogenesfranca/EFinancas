using EFinancas.Dominio.Constantes;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Infraestrutura.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class CategoriasRepositorio : RepositorioBase<Categoria>, ICategoriasRepositorio
    {
        public CategoriasRepositorio(IMongoDatabase database) : base(database, Collections.Categorias)
        {
        }
    }
}
