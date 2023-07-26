namespace EFinancas.Dominio.Interfaces.Servicos
{
    public interface IGerenciamentoCategoriasServico
    {
        Task Atualizar(string id, string categoria);
        Task Inserir(string categoria);
    }
}