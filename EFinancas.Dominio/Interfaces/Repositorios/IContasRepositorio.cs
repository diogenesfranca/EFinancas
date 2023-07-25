using EFinancas.Dominio.Entidades;

namespace EFinancas.Dominio.Interfaces.Repositorios
{
    public interface IContasRepositorio
    {
        Task<IEnumerable<Conta>> Listar();
        Task Inserir(Conta conta);
        Task Atualizar(Conta conta);
        Task Deletar(string id);
    }
}