using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;

namespace EFinancas.Dominio.Servicos
{
    public class GerenciamentoContasServico : IGerenciamentoContasServico
    {
        private readonly IContasRepositorio contasRepositorio;

        public GerenciamentoContasServico(IContasRepositorio contasRepositorio)
        {
            this.contasRepositorio = contasRepositorio;
        }

        public Task Inserir(Models.Conta conta)
        {
            conta.Validar();
            return contasRepositorio.Inserir(conta.Converter());
        }

        public Task Atualizar(string id, Models.Conta conta)
        {
            conta.Validar();
            return contasRepositorio.Atualizar(conta.Converter());
        }
    }
}
