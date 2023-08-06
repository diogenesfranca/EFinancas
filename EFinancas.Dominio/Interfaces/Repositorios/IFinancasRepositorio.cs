using EFinancas.Dominio.Interfaces.Entidades;

namespace EFinancas.Dominio.Interfaces.Repositorios
{
    public interface IFinancasRepositorio<T> : IRepositorioBase<T> where T: IFinanca
    {
        Task<IEnumerable<T>> Listar(string idConta);
    }
}
