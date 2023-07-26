using EFinancas.Dominio.Models;

namespace EFinancas.Dominio.Interfaces.Servicos
{
    public interface IGerenciamentoReceitasServico
    {
        Task Inserir(Receita receita);
        Task Atualizar(string id, Receita receita);
    }
}