namespace EFinancas.Dominio.Interfaces.Servicos
{
    public interface IGerenciamentoCategoriasServico
    {
        Task Inserir(string categoria);
        Task Atualizar(string id, string categoria);
    }
}