using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Models;
using FluentAssertions;
using Xunit;

namespace EFinancas.Testes.Dominio.Models
{
    public class ContaTestes
    {
        [Fact]
        public void Validar_Sucesso()
        {
            //Ação
            var ex = Record.Exception(() => new Conta
            {
                Descricao = "descricao",
                Saldo = 700.35M
            }.Validar());
            
            //Afirmação
            Assert.Null(ex);
        }

        [Theory]
        [InlineData("", "A descrição deve ser preenchida.")]
        [InlineData(null, "A descrição deve ser preenchida.")]
        [InlineData("Descrição longa demais para ser suportada. Muitos caracteres não serão aceitos e gerarão um erro!!!!!", "A descrição não deve ter mais de 100 caracteres.")]
        public void Validar_Descricao_Exception(string descricao, string erro)
        {
            //Ação
            var ex = Assert.Throws<ContaException>(() => new Conta { Descricao = descricao}.Validar());

            //Afirmação
            Assert.Equal(erro, ex.Message);
        }

        [Fact]
        public void Validar_Saldo_Negativo_Exception()
        {
            //Ação
            var ex = Assert.Throws<ContaException>(() => new Conta { Descricao = "descricao", Saldo = -50 }.Validar());

            //Afirmação
            Assert.Equal("O saldo deve ser maior ou igual a 0", ex.Message);
        }

        [Fact]
        public void Converter_Sucesso()
        {
            //Ação
            var conta = new Conta
            {
                Descricao = "descricao",
                Saldo = 87.55M
            }.Converter("4567");

            //Afirmação
            conta.Should().BeEquivalentTo(new EFinancas.Dominio.Entidades.Conta
            {
                Id = "4567",
                Descricao = "descricao",
                Saldo = 87.55M
            });
        }
    }
}
