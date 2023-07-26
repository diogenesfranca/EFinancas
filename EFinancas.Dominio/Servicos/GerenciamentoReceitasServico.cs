using EFinancas.Dominio.Entidades;
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

            return receitasRepositorio.Inserir(new Receita
            {
                Descricao = receita.Descricao,
                Valor = receita.Valor,
                Data = receita.Data,
                IdConta = receita.IdConta,
                IdsCategorias = receita.IdsCategorias
            });
        }

        public Task Atualizar(string id, Models.Receita receita)
        {
            receita.Validar();

            return receitasRepositorio.Atualizar(new Receita
            {
                Id = id,
                Descricao = receita.Descricao,
                Valor = receita.Valor,
                Data = receita.Data,
                IdConta = receita.IdConta,
                IdsCategorias = receita.IdsCategorias
            });
        }
    }
}
