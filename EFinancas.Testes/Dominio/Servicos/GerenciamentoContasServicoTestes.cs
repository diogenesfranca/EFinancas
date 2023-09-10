using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Models;
using EFinancas.Dominio.Servicos;
using Moq;
using Xunit;

namespace EFinancas.Testes.Dominio.Servicos
{
    public class GerenciamentoContasServicoTestes
    {
        private readonly Mock<IContasRepositorio> contasRepositorioMock = new();
        private readonly Mock<Conta> contaMock = new();

        private readonly GerenciamentoContasServico gerenciamentoContasServico;

        public GerenciamentoContasServicoTestes()
        {
            gerenciamentoContasServico = new(contasRepositorioMock.Object);
        }

        [Fact]
        public async Task Inserir_Sucesso()
        {
            //Preparação
            contaMock.Setup(x => x.Converter("")).Returns(new EFinancas.Dominio.Entidades.Conta
            {
                Descricao = "Carteira",
                Saldo = 450.32M
            });

            //Ação
            await gerenciamentoContasServico.Inserir(contaMock.Object);

            //Afirmação
            contasRepositorioMock.Verify(x => x.Inserir(It.Is<EFinancas.Dominio.Entidades.Conta>(x => x.Descricao == "Carteira" && x.Saldo == 450.32M)), Times.Once);
            contaMock.Verify(x => x.Validar(), Times.Once);
            contaMock.Verify(x => x.Converter(""), Times.Once);
        }

        [Fact]
        public async Task Atualizar_Sucesso()
        {
            //Preparação
            contaMock.Setup(x => x.Converter("IdConta1")).Returns(new EFinancas.Dominio.Entidades.Conta
            {
                Id = "IdConta1",
                Descricao = "Carteira",
                Saldo = 753.43M
            });

            //Ação
            await gerenciamentoContasServico.Atualizar("IdConta1", contaMock.Object);

            //Afirmação
            contasRepositorioMock.Verify(x => x.Atualizar(It.Is<EFinancas.Dominio.Entidades.Conta>(x => x.Id == "IdConta1" && x.Descricao == "Carteira" && x.Saldo == 753.43M)), Times.Once);
            contaMock.Verify(x => x.Validar(), Times.Once);
            contaMock.Verify(x => x.Converter("IdConta1"), Times.Once);
        }
    }
}
