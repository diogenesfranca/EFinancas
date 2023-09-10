using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Servicos;
using Moq;
using Xunit;

namespace EFinancas.Testes.Dominio.Servicos
{
    public class GerenciamentoCategoriasServicoTestes
    {
        private readonly Mock<ICategoriasRepositorio> categoriasRepositorioMock = new();

        private readonly GerenciamentoCategoriasServico gerenciamentoCategoriasServico;

        public GerenciamentoCategoriasServicoTestes()
        {
            gerenciamentoCategoriasServico = new(categoriasRepositorioMock.Object);
        }

        [Fact]
        public async Task Inserir_Sucesso()
        {
            //Ação
            await gerenciamentoCategoriasServico.Inserir("Financas");

            //Afirmação
            categoriasRepositorioMock.Verify(x => x.Inserir(It.Is<Categoria>(x => x.Descricao == "Financas")), Times.Once);
        }

        [Fact]
        public async Task Atualizar_Sucesso()
        {
            //Ação
            await gerenciamentoCategoriasServico.Atualizar("IdCategoria1", "Financas");

            //Afirmação
            categoriasRepositorioMock.Verify(x => x.Atualizar(It.Is<Categoria>(x => x.Id == "IdCategoria1" && x.Descricao == "Financas")), Times.Once);
        }

        [Theory]
        [InlineData("", "A categoria deve ser preenchida.")]
        [InlineData(null, "A categoria deve ser preenchida.")]
        [InlineData("Descrição longa demais para ser suportada. Muitos caracteres não serão aceitos e gerarão um erro!!!!!", "A categoria não deve ter mais de 100 caracteres.")]
        public async Task Inserir_CategoriaExcetpion(string descricao, string erro)
        {
            //Ação
            var ex = await Assert.ThrowsAsync<CategoriaException>(() =>  gerenciamentoCategoriasServico.Inserir(descricao));

            //Afirmação
            Assert.Equal(erro, ex.Message);

            categoriasRepositorioMock.Verify(x => x.Inserir(It.IsAny<Categoria>()), Times.Never);
        }
    }
}
