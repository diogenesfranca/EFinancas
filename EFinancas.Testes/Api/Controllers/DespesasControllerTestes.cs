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
using DespesaEntidade = EFinancas.Dominio.Entidades.Despesa;

namespace EFinancas.Testes.Api.Controllers
{
    public class DespesasControllerTestes
    {
        private readonly Mock<ILogger<DespesasController>> loggerMock = new();
        private readonly Mock<IDespesasRepositorio> despesasRepositorioMock = new();
        private readonly Mock<IGerenciamentoDespesasServico> gerenciamentoDespesasServicoMock = new();

        private readonly Despesa despesa;
        private readonly DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
        private readonly DespesasController controller;


        public DespesasControllerTestes()
        {
            despesa = new()
            {
                Descricao = "Sorvete",
                IdConta = "IdConta1",
                Valor = 15.76M,
                Data = hoje,
                IdsCategorias = new[] { "IdCategoria1", "IdCategoria2" }
            };

            controller = new DespesasController(loggerMock.Object, despesasRepositorioMock.Object, gerenciamentoDespesasServicoMock.Object);
        }

        [Fact]
        public async Task Get_Sucesso()
        {
            //Preparação
            despesasRepositorioMock.Setup(x => x.Listar()).ReturnsAsync(new List<DespesaEntidade>
            {
                new DespesaEntidade { Id = "12345", Descricao = "Sorvete", IdConta = "IdConta1", Valor = 10.35M },
                new DespesaEntidade { Id = "45647", Descricao = "Pastel", IdConta = "IdConta2", Valor = 7.65M }
            });

            //Ação
            var resultado = await controller.Get() as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            resultado.Value.Should().BeEquivalentTo(new List<DespesaEntidade>
            {
                new DespesaEntidade { Id = "12345", Descricao = "Sorvete", IdConta = "IdConta1", Valor = 10.35M },
                new DespesaEntidade { Id = "45647", Descricao = "Pastel", IdConta = "IdConta2", Valor = 7.65M }
            });

            despesasRepositorioMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public async Task Post_Sucesso()
        {
            //Ação
            var resultado = await controller.Post(despesa) as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoDespesasServicoMock.Verify(x => x.Inserir(
                It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Post_FinancaException()
        {
            //Preparação
            gerenciamentoDespesasServicoMock.Setup(x => x.Inserir(It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new FinancaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Post(despesa) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoDespesasServicoMock.Verify(x => x.Inserir(It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Post_InternalServerError()
        {
            //Preparação
            gerenciamentoDespesasServicoMock.Setup(x => x.Inserir(It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Post(despesa) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoDespesasServicoMock.Verify(x => x.Inserir(It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_Sucesso()
        {
            //Ação
            var resultado = await controller.Put("id", despesa) as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoDespesasServicoMock.Verify(x => x.Atualizar("id", It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Put_FinancaException()
        {
            //Preparação
            gerenciamentoDespesasServicoMock.Setup(x => x.Atualizar("id", It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje
                && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new FinancaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", despesa) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoDespesasServicoMock.Verify(x => x.Atualizar("id", It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_InternalServerError()
        {
            //Preparação
            gerenciamentoDespesasServicoMock.Setup(x => x.Atualizar("id", It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2"))).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", despesa) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoDespesasServicoMock.Verify(x => x.Atualizar("id", It.Is<Despesa>(x => x.Descricao == "Sorvete" && x.IdConta == "IdConta1" && x.Valor == 15.76M && x.Data == hoje && x.IdsCategorias.Count() == 2 && x.IdsCategorias.First() == "IdCategoria1" && x.IdsCategorias.Last() == "IdCategoria2")), Times.Once);

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

            despesasRepositorioMock.Verify(x => x.Deletar("id"), Times.Once);
        }
    }
}
