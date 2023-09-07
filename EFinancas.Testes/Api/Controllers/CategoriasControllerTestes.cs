using EFinancas.Api.Controllers;
using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Xunit;

namespace EFinancas.Testes.Api.Controllers
{
    public class CategoriasControllerTestes
    {
        private readonly Mock<ILogger<CategoriasController>> loggerMock = new();
        private readonly Mock<ICategoriasRepositorio> categoriasRepositorioMock = new();
        private readonly Mock<IGerenciamentoCategoriasServico> gerenciamentoCategoriasServicoMock = new();

        private readonly CategoriasController controller;

        public CategoriasControllerTestes()
        {
            controller = new CategoriasController(loggerMock.Object, categoriasRepositorioMock.Object, gerenciamentoCategoriasServicoMock.Object);
        }

        [Fact]
        public async Task Get_Sucesso()
        {
            //Ação
            var resultado = await controller.Get() as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            categoriasRepositorioMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public async Task Post_Sucesso()
        {
            //Ação
            var resultado = await controller.Post("financas") as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoCategoriasServicoMock.Verify(x => x.Inserir("financas"), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Post_CategoriaException()
        {
            //Preparação
            gerenciamentoCategoriasServicoMock.Setup(x => x.Inserir("xpto")).Throws(new CategoriaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Post("xpto") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoCategoriasServicoMock.Verify(x => x.Inserir("xpto"), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Post_InternalServerError()
        {
            //Preparação
            gerenciamentoCategoriasServicoMock.Setup(x => x.Inserir("xpto")).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Post("xpto") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoCategoriasServicoMock.Verify(x => x.Inserir("xpto"), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_Sucesso()
        {
            //Ação
            var resultado = await controller.Put("id", "financas") as OkResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            gerenciamentoCategoriasServicoMock.Verify(x => x.Atualizar("id", "financas"), Times.Once);

            loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
        }

        [Fact]
        public async Task Put_CategoriaException()
        {
            //Preparação
            gerenciamentoCategoriasServicoMock.Setup(x => x.Atualizar("id", "xpto")).Throws(new CategoriaException("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", "xpto") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoCategoriasServicoMock.Verify(x => x.Atualizar("id", "xpto"), Times.Once);

            loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => o.ToString() == "Erro qualquer"), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once
            );
        }

        [Fact]
        public async Task Put_InternalServerError()
        {
            //Preparação
            gerenciamentoCategoriasServicoMock.Setup(x => x.Atualizar("id", "xpto")).Throws(new Exception("Erro qualquer"));

            //Ação
            var resultado = await controller.Put("id", "xpto") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.InternalServerError, resultado!.StatusCode);
            Assert.Equal("Erro qualquer", resultado.Value);

            gerenciamentoCategoriasServicoMock.Verify(x => x.Atualizar("id", "xpto"), Times.Once);

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

            categoriasRepositorioMock.Verify(x => x.Deletar("id"), Times.Once);
        }
    }
}
