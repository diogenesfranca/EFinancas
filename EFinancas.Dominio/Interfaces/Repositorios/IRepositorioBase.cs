namespace EFinancas.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<T>
    {
        Task<T> Obter(string id);
        Task<IEnumerable<T>> Listar();
        Task Inserir(T entidade);
        Task Atualizar(T entidade);
        Task Deletar(string id);
    }
}
