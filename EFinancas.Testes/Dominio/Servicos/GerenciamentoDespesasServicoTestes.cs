using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Models;
using EFinancas.Dominio.Servicos;
using Moq;
using Xunit;

namespace EFinancas.Testes.Dominio.Servicos
{
    public class GerenciamentoDespesasServicoTestes
    {
        private readonly Mock<IDespesasRepositorio> despesasRepositorioMock = new();
        private readonly Mock<Despesa> despesaMock = new();

        private readonly GerenciamentoDespesasServico gerenciamentoDespesasServico;

        public GerenciamentoDespesasServicoTestes()
        {
            gerenciamentoDespesasServico = new(despesasRepositorioMock.Object);
        }

        [Fact]
        public async Task Inserir_Sucesso()
        {
            //Preparação
            var data = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            despesaMock.Setup(x => x.Converter("")).Returns(new EFinancas.Dominio.Entidades.Despesa
            {
                Descricao = "Sorvete",
                Valor = 10.35M,
                Data = data,
                IdConta = "IdConta1",
                IdsCategorias = new[] { "Id1", "Id2" }
            });

            //Ação
            await gerenciamentoDespesasServico.Inserir(despesaMock.Object);

            //Afirmação
            despesasRepositorioMock.Verify(x => x.Inserir(It.Is<EFinancas.Dominio.Entidades.Despesa>(x => x.Descricao == "Sorvete" && x.Valor == 10.35M && x.Data == data && x.IdConta == "IdConta1" && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "Id1" && x.IdsCategorias.Last() == "Id2")), Times.Once);
            despesaMock.Verify(x => x.Validar(), Times.Once);
            despesaMock.Verify(x => x.Converter(""), Times.Once);
        }

        [Fact]
        public async Task Atualizar_Sucesso()
        {
            //Preparação
            var data = DateOnly.FromDateTime(DateTime.Now.AddDays(-2));
            despesaMock.Setup(x => x.Converter("IdDespesa1")).Returns(new EFinancas.Dominio.Entidades.Despesa
            {
                Id = "IdDespesa1",
                Descricao = "Sorvete",
                Valor = 10.35M,
                Data = data,
                IdConta = "IdConta1",
                IdsCategorias = new[] { "Id1", "Id2" }
            });

            //Ação
            await gerenciamentoDespesasServico.Atualizar("IdDespesa1", despesaMock.Object);

            //Afirmação
            despesasRepositorioMock.Verify(x => x.Atualizar(It.Is<EFinancas.Dominio.Entidades.Despesa>(x => x.Id == "IdDespesa1" && x.Descricao == "Sorvete" && x.Valor == 10.35M && x.IdConta == "IdConta1" && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "Id1" && x.IdsCategorias.Last() == "Id2")), Times.Once);
            despesaMock.Verify(x => x.Validar(), Times.Once);
            despesaMock.Verify(x => x.Converter("IdDespesa1"), Times.Once);
        }
    }
}
