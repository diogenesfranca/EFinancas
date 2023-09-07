using EFinancas.Api.Controllers;
using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Xunit;

namespace EFinancas.Testes.Api.Controllers
{
    public class ContasControllerTestes
    {
        private readonly Mock<ILogger<ContasController>> loggerMock = new();
        private readonly Mock<IContasRepositorio> contasRepositorioMock = new();
        private readonly Mock<IGerenciamentoContasServico> gerenciamentoContasServicoMock = new();
        
        private readonly Conta conta;
        private readonly ContasController controller;
        

        public ContasControllerTestes()
        {
            conta = new()
            {
                Descricao = "Banco",
                Saldo = 600.45M
            };

            controller = new ContasController(loggerMock.Object, contasRepositorioMock.Object, gerenciamentoContasServicoMock.Object);
        }

        [Fact]
        public async Task Get_Sucesso()
        {
            //Ação
            var resultado = await controller.Get() as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);
            contasRepositorioMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public async Task Post_Sucesso()
        {
            //Ação
            var resultado = await controller.Post(conta) as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoContasServicoMock.Verify(x => x.Inserir(It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M)), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Post_ContaException()
        {
            //Preparação
            gerenciamentoContasServicoMock.Setup(x => x.Inserir(It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M))).Throws(new ContaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Post(conta) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoContasServicoMock.Verify(x => x.Inserir(It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M)), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Post_InternalServerError()
        {
            //Preparação
            gerenciamentoContasServicoMock.Setup(x => x.Inserir(It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M))).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Post(conta) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoContasServicoMock.Verify(x => x.Inserir(It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M)), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_Sucesso()
        {
            //Ação
            var resultado = await controller.Put("id", conta) as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoContasServicoMock.Verify(x => x.Atualizar("id", It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M)), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Put_ContaException()
        {
            //Preparação
            gerenciamentoContasServicoMock.Setup(x => x.Atualizar("id", It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M))).Throws(new ContaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", conta) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoContasServicoMock.Verify(x => x.Atualizar("id", It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M)), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_InternalServerError()
        {
            //Preparação
            gerenciamentoContasServicoMock.Setup(x => x.Atualizar("id", It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M))).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", conta) as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoContasServicoMock.Verify(x => x.Atualizar("id", It.Is<Conta>(x => x.Descricao == "Banco" && x.Saldo == 600.45M)), Times.Once);

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

            contasRepositorioMock.Verify(x => x.Deletar("id"), Times.Once);
        }
    }
}
