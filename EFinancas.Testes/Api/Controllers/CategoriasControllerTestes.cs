using EFinancas.Api.Controllers;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using FluentAssertions;
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
            controller = new(loggerMock.Object, categoriasRepositorioMock.Object, gerenciamentoCategoriasServicoMock.Object);
        }

        [Fact]
        public async Task Get_Por_Id_Sucesso()
        {
            //Preparação
            categoriasRepositorioMock.Setup(x => x.Obter("64c064883c6e31cfcbf86e52")).ReturnsAsync( new Categoria { Descricao = "Finanças", Id = "12345" });

            //Ação
            var resultado = await controller.Get("64c064883c6e31cfcbf86e52") as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            resultado.Value.Should().BeEquivalentTo(new Categoria { Descricao = "Finanças", Id = "12345" });

            categoriasRepositorioMock.Verify(x => x.Obter("64c064883c6e31cfcbf86e52"), Times.Once);
        }

        [Fact]
        public async Task Get_Por_Id_Invalido_Erro()
        {
            //Ação
            var resultado = await controller.Get("64c064883c6e31cfcbf") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.BadRequest, resultado!.StatusCode);

            Assert.Equal("Id inválido, por favor forneça um id no formato correto de 24 caracteres hexadecimais.", resultado.Value);

            categoriasRepositorioMock.Verify(x => x.Obter(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Get_Por_Id_Nao_Encontrado_Sucesso()
        {
            //Ação
            var resultado = await controller.Get("64c064883c6e31cfcbf75e34") as ObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.NotFound, resultado!.StatusCode);

            Assert.Equal("Categoria não encontrada.", resultado.Value);

            categoriasRepositorioMock.Verify(x => x.Obter("64c064883c6e31cfcbf75e34"), Times.Once);
        }

        [Fact]
        public async Task Get_Sucesso()
        {
            //Preparação
            categoriasRepositorioMock.Setup(x => x.Listar()).ReturnsAsync(new List<Categoria> { new Categoria { Descricao = "Finanças", Id = "12345" }, new Categoria { Descricao = "Mercado", Id = "45678" } });

            //Ação
            var resultado = await controller.Get() as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            resultado.Value.Should().BeEquivalentTo(new List<Categoria> { new Categoria { Descricao = "Finanças", Id = "12345" }, new Categoria { Descricao = "Mercado", Id = "45678" } });

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
