using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Models;

namespace EFinancas.Dominio.Servicos
{
    public class ListagemHistoricoFinancasServico : IListagemHistoricoFinancasServico
    {
        private readonly IReceitasRepositorio receitasRepositorio;
        private readonly IDespesasRepositorio despesasRepositorio;

        public ListagemHistoricoFinancasServico(IReceitasRepositorio receitasRepositorio, IDespesasRepositorio despesasRepositorio)
        {
            this.receitasRepositorio = receitasRepositorio;
            this.despesasRepositorio = despesasRepositorio;
        }

        public async Task<IEnumerable<HistoricoFinanca>> Listar()
        {
            List<Financa> financas = new();

            var receitasTask = receitasRepositorio.Listar();
            var despesasTask = despesasRepositorio.Listar();

            financas.AddRange(await receitasTask);
            financas.AddRange(await despesasTask);

            return financas.OrderByDescending(x => x.Data).Select(x => new HistoricoFinanca
            {
                Id = x.Id,
                Data = x.Data,
                Descricao = x.Descricao,
                Valor = x.GetType() == typeof(Entidades.Despesa) ? -x.Valor : x.Valor
            });
        }

        public async Task<IEnumerable<HistoricoFinanca>> Listar(string idConta)
        {
            List<Financa> financas = new();

            var receitasTask = receitasRepositorio.Listar(idConta);
            var despesasTask = despesasRepositorio.Listar(idConta);

            financas.AddRange(await receitasTask);
            financas.AddRange(await despesasTask);

            return financas.OrderByDescending(x => x.Data).Select(x => new HistoricoFinanca
            {
                Id = x.Id,
                Data = x.Data,
                Descricao = x.Descricao,
                Valor = x.GetType() == typeof(Entidades.Despesa) ? -x.Valor : x.Valor
            });
        }
    }
}
