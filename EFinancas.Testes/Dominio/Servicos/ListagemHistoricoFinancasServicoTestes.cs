using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Servicos;
using FluentAssertions;
using Moq;
using Xunit;

namespace EFinancas.Testes.Dominio.Servicos
{
    public class ListagemHistoricoFinancasServicoTestes
    {
        private readonly Mock<IReceitasRepositorio> receitasRepositorioMock = new();
        private readonly Mock<IDespesasRepositorio> despesasRepositorioMock = new();

        private readonly ListagemHistoricoFinancasServico listagemHistoricoFinancasServico;

        public ListagemHistoricoFinancasServicoTestes()
        {
            listagemHistoricoFinancasServico = new(receitasRepositorioMock.Object, despesasRepositorioMock.Object);
        }

        [Fact]
        public async Task Listar_Sucesso()
        {
            //Preparação
            var ontem = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            var anteOntem = DateOnly.FromDateTime(DateTime.Now.AddDays(-2));
           
            receitasRepositorioMock.Setup(x => x.Listar()).ReturnsAsync(new List<Receita>
            {
                new Receita
                {
                    Id = "4567",
                    Data = ontem,
                    Descricao = "Salario",
                    Valor = 4567.82M
                }
            });

            despesasRepositorioMock.Setup(x => x.Listar()).ReturnsAsync(new List<Despesa>
            {
                new Despesa
                {
                    Id = "8793",
                    Data = anteOntem,
                    Descricao = "Sorvete",
                    Valor = 10.45M
                }
            });

            //Ação
            var historico = await listagemHistoricoFinancasServico.Listar();

            //Afirmação
            historico.Should().BeEquivalentTo(new List<EFinancas.Dominio.Models.HistoricoFinanca>
            {
                new EFinancas.Dominio.Models.HistoricoFinanca
                {
                    Id = "8793",
                    Data = anteOntem,
                    Descricao = "Sorvete",
                    Valor = -10.45M
                },
                new EFinancas.Dominio.Models.HistoricoFinanca
                {
                    Id = "4567",
                    Data = ontem,
                    Descricao = "Salario",
                    Valor = 4567.82M
                }
            });
            
            receitasRepositorioMock.Verify(x => x.Listar(), Times.Once);
            despesasRepositorioMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public async Task Listar_PorId_Sucesso()
        {
            //Preparação
            var ontem = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            var anteOntem = DateOnly.FromDateTime(DateTime.Now.AddDays(-2));

            receitasRepositorioMock.Setup(x => x.Listar("Id")).ReturnsAsync(new List<Receita>
            {
                new Receita
                {
                    Id = "4567",
                    Data = ontem,
                    Descricao = "Salario",
                    Valor = 4567.82M
                }
            });

            despesasRepositorioMock.Setup(x => x.Listar("Id")).ReturnsAsync(new List<Despesa>
            {
                new Despesa
                {
                    Id = "8793",
                    Data = anteOntem,
                    Descricao = "Sorvete",
                    Valor = 10.45M
                }
            });

            //Ação
            var historico = await listagemHistoricoFinancasServico.Listar("Id");

            //Afirmação
            historico.Should().BeEquivalentTo(new List<EFinancas.Dominio.Models.HistoricoFinanca>
            {
                new EFinancas.Dominio.Models.HistoricoFinanca
                {
                    Id = "8793",
                    Data = anteOntem,
                    Descricao = "Sorvete",
                    Valor = -10.45M
                },
                new EFinancas.Dominio.Models.HistoricoFinanca
                {
                    Id = "4567",
                    Data = ontem,
                    Descricao = "Salario",
                    Valor = 4567.82M
                }
            });

            receitasRepositorioMock.Verify(x => x.Listar("Id"), Times.Once);
            despesasRepositorioMock.Verify(x => x.Listar("Id"), Times.Once);
        }
    }
}
