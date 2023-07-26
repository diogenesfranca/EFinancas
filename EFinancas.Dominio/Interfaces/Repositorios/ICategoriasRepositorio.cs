using EFinancas.Dominio.Entidades;

namespace EFinancas.Dominio.Interfaces.Repositorios
{
    public interface ICategoriasRepositorio
    {
        Task<IEnumerable<Categoria>> Listar();
        Task Inserir(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Deletar(string id);
    }
}