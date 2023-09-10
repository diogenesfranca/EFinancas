using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Models;
using EFinancas.Dominio.Servicos;
using Moq;
using Xunit;

namespace EFinancas.Testes.Dominio.Servicos
{
    public class GerenciamentoReceitasServicoTestes
    {
        private readonly Mock<IReceitasRepositorio> receitasRepositorioMock = new();
        private readonly Mock<Receita> receitaMock = new();

        private readonly GerenciamentoReceitasServico gerenciamentoReceitasServico;

        public GerenciamentoReceitasServicoTestes()
        {
            gerenciamentoReceitasServico = new(receitasRepositorioMock.Object);
        }

        [Fact]
        public async Task Inserir_Sucesso()
        {
            //Preparação
            var data = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            receitaMock.Setup(x => x.Converter("")).Returns(new EFinancas.Dominio.Entidades.Receita
            {
                Descricao = "Salario",
                Valor = 4530.71M,
                Data = data,
                IdConta = "IdConta1",
                IdsCategorias = new[] { "Id1", "Id2" }
            });

            //Ação
            await gerenciamentoReceitasServico.Inserir(receitaMock.Object);

            //Afirmação
            receitasRepositorioMock.Verify(x => x.Inserir(It.Is<EFinancas.Dominio.Entidades.Receita>(x => x.Descricao == "Salario" && x.Valor == 4530.71M && x.Data == data && x.IdConta == "IdConta1" && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "Id1" && x.IdsCategorias.Last() == "Id2")), Times.Once);
            receitaMock.Verify(x => x.Validar(), Times.Once);
            receitaMock.Verify(x => x.Converter(""), Times.Once);
        }

        [Fact]
        public async Task Atualizar_Sucesso()
        {
            //Preparação
            var data = DateOnly.FromDateTime(DateTime.Now.AddDays(-2));
            receitaMock.Setup(x => x.Converter("IdReceita1")).Returns(new EFinancas.Dominio.Entidades.Receita
            {
                Id = "IdReceita1",
                Descricao = "Salario",
                Valor = 4256.23M,
                Data = data,
                IdConta = "IdConta1",
                IdsCategorias = new[] { "Id1", "Id2" }
            });

            //Ação
            await gerenciamentoReceitasServico.Atualizar("IdReceita1", receitaMock.Object);

            //Afirmação
            receitasRepositorioMock.Verify(x => x.Atualizar(It.Is<EFinancas.Dominio.Entidades.Receita>(x => x.Id == "IdReceita1" && x.Descricao == "Salario" && x.Valor == 4256.23M && x.IdConta == "IdConta1" && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "Id1" && x.IdsCategorias.Last() == "Id2")), Times.Once);
            receitaMock.Verify(x => x.Validar(), Times.Once);
            receitaMock.Verify(x => x.Converter("IdReceita1"), Times.Once);
        }
    }
}
