using EFinancas.Dominio.Models;

namespace EFinancas.Dominio.Interfaces.Servicos
{
    public interface IGerenciamentoDespesasServico
    {
        Task Inserir(Despesa despesa);
        Task Atualizar(string id, Despesa despesa);
    }
}
