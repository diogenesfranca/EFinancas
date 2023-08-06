using EFinancas.Dominio.Models;

namespace EFinancas.Dominio.Interfaces.Servicos
{
    public interface IListagemHistoricoFinancasServico
    {
        Task<IEnumerable<HistoricoFinanca>> Listar();
        Task<IEnumerable<HistoricoFinanca>> Listar(string idConta);
    }
}