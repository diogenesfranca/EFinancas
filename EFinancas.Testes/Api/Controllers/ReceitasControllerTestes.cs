using EFinancas.Api.Controllers;
using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Xunit;
using ReceitaEntidade = EFinancas.Dominio.Entidades.Receita;

namespace EFinancas.Testes.Api.Controllers
{
    public class ReceitasControllerTestes
    {
        private readonly Mock<ILogger<ReceitasController>> loggerMock = new();
        private readonly Mock<IReceitasRepositorio> receitasRepositorioMock = new();
        private readonly Mock<IGerenciamentoReceitasServico> gerenciamentoReceitasServicoMock = new();

        private readonly Receita receita;
        private readonly DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
        private readonly ReceitasController controller;


        public ReceitasControllerTestes()
        {
            receita = new()
            {
                Descricao = "Salario",
                IdConta = "IdConta1",
                Valor = 5347.95M,
                Data = hoje,
                IdsCategorias = new[] { "IdCategoria1", "IdCategoria2" }
            };

            controller = new(loggerMock.Object, receitasRepositorioMock.Object, gerenciamentoReceitasServicoMock.Object);
        }

        [Fact]
        public async Task Get_Por_Id_Sucesso()
        {
            //Preparação
            receitasRepositorioMock.Setup(x => x.Obter("64c064883c6e31cfcbf86e52")).ReturnsAsync(new ReceitaEntidade { Id = "12345", Descricao = "Salario", IdConta = "IdConta1", Valor = 5456.43M });

            //Ação
            var resultado = await controller.Get("64c064883c6e31cfcbf86e52") as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            resultado.Value.Should().BeEquivalentTo(new ReceitaEntidade { Id = "12345", Descricao = "Salario", IdConta = "IdConta1", Valor = 5456.43M });

            receitasRepositorioMock.Verify(x => x.Obter("64c064883c6e31cfcbf86e52"), Times.Once);
        }

        [Fact]
        public async Task Get_Por_Id_Invalido_Erro()
        {
            //Ação
            var resultado = await controller.Get("64c064883c6e31cfcbf") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);

            Assert.Equal("Id inválido, por favor forneça um id no formato correto de 24 caracteres hexadecimais.", resultado.Value);

            receitasRepositorioMock.Verify(x => x.Obter(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Get_Por_Id_Nao_Encontrado_Sucesso()
        {
            //Ação
            var resultado = await controller.Get("64c064883c6e31cfcbf75e34") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.NotFound, resultado!.StatusCode);

            Assert.Equal("Receita não encontrada.", resultado.Value);

            receitasRepositorioMock.Verify(x => x.Obter("64c064883c6e31cfcbf75e34"), Times.Once);
        }

        [Fact]
        public async Task Get_Sucesso()
        {
            //Preparação
            receitasRepositorioMock.Setup(x => x.Listar()).ReturnsAsync(new List<ReceitaEntidade>
            {
                new ReceitaEntidade { Id = "12345", Descricao = "Salario", IdConta = "IdConta1", Valor = 5456.43M },
                new ReceitaEntidade { Id = "45647", Descricao = "Bonus", IdConta = "IdConta2", Valor = 500 }
            });

            //Ação
            var resultado = await controller.Get() as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            resultado.Value.Should().BeEquivalentTo(new List<ReceitaEntidade>
            {
                new ReceitaEntidade { Id = "12345", Descricao = "Salario", IdConta = "IdConta1", Valor = 5456.43M },
                new ReceitaEntidade { Id = "45647", Descricao = "Bonus", IdConta = "IdConta2", Valor = 500 }
            });

            receitasRepositorioMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public async Task Post_Sucesso()
        {
            //Ação
            var resultado = await controller.Post(receita) as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoReceitasServicoMock.Verify(x => x.Inserir(
                It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Post_FinancaException()
        {
            //Preparação
            gerenciamentoReceitasServicoMock.Setup(x => x.Inserir(It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new FinancaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Post(receita) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoReceitasServicoMock.Verify(x => x.Inserir(It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Post_InternalServerError()
        {
            //Preparação
            gerenciamentoReceitasServicoMock.Setup(x => x.Inserir(It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Post(receita) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoReceitasServicoMock.Verify(x => x.Inserir(It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_Sucesso()
        {
            //Ação
            var resultado = await controller.Put("id", receita) as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoReceitasServicoMock.Verify(x => x.Atualizar("id", It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Put_FinancaException()
        {
            //Preparação
            gerenciamentoReceitasServicoMock.Setup(x => x.Atualizar("id", It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new FinancaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", receita) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoReceitasServicoMock.Verify(x => x.Atualizar("id", It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_InternalServerError()
        {
            //Preparação
            gerenciamentoReceitasServicoMock.Setup(x => x.Atualizar("id", It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", receita) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoReceitasServicoMock.Verify(x => x.Atualizar("id", It.Is<Receita>(x => x.Descricao == "Salario" && x.IdConta == "IdConta1" && x.Valor == 5347.95M && x.Data == hoje && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Delete_Sucesso()
        {
            //Ação
            var resultado = await controller.Delete("id") as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            receitasRepositorioMock.Verify(x => x.Deletar("id"), Times.Once);
        }
    }
}
