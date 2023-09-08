using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Models;
using FluentAssertions;
using Xunit;

namespace EFinancas.Testes.Dominio.Models
{
    public class FinancaTestes
    {
        [Fact]
        public void Validar_Sucesso()
        {
            //Ação
            var ex = Record.Exception(() => new Receita
            {
                Descricao = "descricao",
                Valor = 700.35M,
                Data = DateOnly.FromDateTime(DateTime.Now),
                IdConta = "45678"
            }.Validar());

            //Afirmação
            Assert.Null(ex);
        }

        [Theory]
        [InlineData("", "A descrição deve ser preenchida.")]
        [InlineData(null, "A descrição deve ser preenchida.")]
        [InlineData("Descrição longa demais para ser suportada. Muitos caracteres não serão aceitos e gerarão um erro!!!!!\r\nDescrição longa demais para ser suportada. Muitos caracteres não serão aceitos e gerarão um erro!!!!!\r\nDescrição longa demais para ser suportada. Muitos caracteres não serão aceitos e gerarão um erro!!!!!\r\nDescrição longa demais para ser suportada. Muitos caracteres não serão aceitos e gerarão um erro!!!!!\r\nDescrição longa demais para ser suportada. Muitos caracteres não serão aceitos e gerarão um erro!!!!!", "A descrição não deve ter mais de 500 caracteres.")]
        public void Validar_Descricao_Exception(string descricao, string erro)
        {
            //Ação
            var ex = Assert.Throws<FinancaException>(() => new Receita { Descricao = descricao }.Validar());

            //Afirmação
            Assert.Equal(erro, ex.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void Validar_Saldo_Zerado_Ou_Negativo_Exception(decimal valor)
        {
            //Ação
            var ex = Assert.Throws<FinancaException>(() => new Receita { Descricao = "descricao", Valor = valor }.Validar());

            //Afirmação
            Assert.Equal("O valor deve ser maior que 0", ex.Message);
        }

        [Fact]
        public void Validar_Conta_Nao_Informada_Exception()
        {
            //Ação
            var ex = Assert.Throws<FinancaException>(() => new Receita
            {
                Descricao = "descricao",
                Valor = 700.35M,
                Data = DateOnly.FromDateTime(DateTime.Now)
            }.Validar());

            //Afirmação
            Assert.Equal("A conta deve ser informada.", ex.Message);
        }

        [Fact]
        public void Validar_Data_Nao_Informada_Exception()
        {
            //Ação
            var ex = Assert.Throws<FinancaException>(() => new Receita
            {
                Descricao = "descricao",
                Valor = 700.35M,                
                IdConta = "45678"
            }.Validar());

            //Afirmação
            Assert.Equal("A data deve ser preenchida.", ex.Message);
        }

        [Fact]
        public void Validar_Data_Futuro_Exception()
        {
            //Ação
            var ex = Assert.Throws<FinancaException>(() => new Receita
            {
                Descricao = "descricao",
                Valor = 700.35M,
                Data = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                IdConta = "45678"
            }.Validar());

            //Afirmação
            Assert.Equal("A receita não pode ser cadastrada no futuro.", ex.Message);
        }

        [Fact]
        public void Converter_Sucesso()
        {
            //Ação
            var receita = new Receita
            {
                Descricao = "descricao",
                Valor = 700.35M,
                Data = DateOnly.FromDateTime(DateTime.Now),
                IdConta = "45678",
                IdsCategorias = new[] { "12", "34" }
            }.Converter("123456");

            //Afirmação
            receita.Should().BeEquivalentTo(new EFinancas.Dominio.Entidades.Receita
            {
                Descricao = "descricao",
                Valor = 700.35M,
                Data = DateOnly.FromDateTime(DateTime.Now),
                IdConta = "45678",
                IdsCategorias = new[] { "12", "34" },
                Id = "123456"
            });
        }
    }
}
