namespace EFinancas.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<T>
    {
        Task<IEnumerable<T>> Listar();
        Task Inserir(T entidade);
        Task Atualizar(T entidade);
        Task Deletar(string id);
    }
}
