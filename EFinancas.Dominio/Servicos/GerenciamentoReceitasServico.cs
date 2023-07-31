using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;

namespace EFinancas.Dominio.Servicos
{
    public class GerenciamentoReceitasServico : IGerenciamentoReceitasServico
    {
        private readonly IReceitasRepositorio receitasRepositorio;

        public GerenciamentoReceitasServico(IReceitasRepositorio receitasRepositorio)
        {
            this.receitasRepositorio = receitasRepositorio;
        }

        public Task Inserir(Models.Receita receita)
        {
            receita.Validar();
            return receitasRepositorio.Inserir(receita.Converter());
        }

        public Task Atualizar(string id, Models.Receita receita)
        {
            receita.Validar();
            return receitasRepositorio.Atualizar(receita.Converter());
        }
    }
}
