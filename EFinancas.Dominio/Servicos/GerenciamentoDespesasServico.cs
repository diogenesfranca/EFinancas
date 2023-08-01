using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;

namespace EFinancas.Dominio.Servicos
{
    public class GerenciamentoDespesasServico : IGerenciamentoDespesasServico
    {
        private readonly IDespesasRepositorio despesasRepositorio;

        public GerenciamentoDespesasServico(IDespesasRepositorio despesasRepositorio)
        {
            this.despesasRepositorio = despesasRepositorio;
        }

        public Task Inserir(Models.Despesa despesa)
        {
            despesa.Validar();
            return despesasRepositorio.Inserir(despesa.Converter());
        }

        public Task Atualizar(string id, Models.Despesa despesa)
        {
            despesa.Validar();
            return despesasRepositorio.Atualizar(despesa.Converter(id));
        }
    }
}
