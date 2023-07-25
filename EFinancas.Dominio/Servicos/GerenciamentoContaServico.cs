using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;

namespace EFinancas.Dominio.Servicos
{
    public class GerenciamentoContaServico : IGerenciamentoContaServico
    {
        private readonly IContasRepositorio contasRepositorio;

        public GerenciamentoContaServico(IContasRepositorio contasRepositorio)
        {
            this.contasRepositorio = contasRepositorio;
        }

        public Task Inserir(Models.Conta conta)
        {
            conta.Validar();

            return contasRepositorio.Inserir(new Conta
            {
                Descricao = conta.Descricao,
                Saldo = conta.Saldo
            });
        }

        public Task Atualizar(string id, Models.Conta conta)
        {
            conta.Validar();

            return contasRepositorio.Atualizar(new Conta
            {
                Id = id,
                Descricao = conta.Descricao,
                Saldo = conta.Saldo
            });
        }
    }
}
