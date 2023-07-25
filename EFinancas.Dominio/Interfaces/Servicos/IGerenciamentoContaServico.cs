using EFinancas.Dominio.Models;

namespace EFinancas.Dominio.Interfaces.Servicos
{
    public interface IGerenciamentoContaServico
    {
        Task Inserir(Conta conta);
        Task Atualizar(string id, Conta conta);
    }
}