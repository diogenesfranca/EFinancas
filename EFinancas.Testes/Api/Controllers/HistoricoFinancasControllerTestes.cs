using EFinancas.Api.Controllers;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace EFinancas.Testes.Api.Controllers
{
    public class HistoricoFinancasControllerTestes
    {
        private readonly Mock<IListagemHistoricoFinancasServico> listagemHistoricoFinancasServicoMock = new();

        private readonly HistoricoFinancasController controller;

        public HistoricoFinancasControllerTestes()
        {
            controller = new(listagemHistoricoFinancasServicoMock.Object);
        }

        [Fact]
        public async Task Get_Sucesso()
        {
            //Preparação
            listagemHistoricoFinancasServicoMock.Setup(x => x.Listar()).ReturnsAsync(new List<HistoricoFinanca>
            {
                new HistoricoFinanca
                {
                    Id = "12345",
                    Descricao = "Salario",
                    Data = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                    Valor = 5456.32M
                },
                new HistoricoFinanca
                {
                    Id = "45647",
                    Descricao = "Sorvete",
                    Data = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                    Valor = 12.36M
                },
            });

            //Ação
            var resultado = await controller.Get() as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            resultado.Value.Should().BeEquivalentTo(new List<HistoricoFinanca>
            {
                new HistoricoFinanca
                {
                    Id = "12345",
                    Descricao = "Salario",
                    Data = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                    Valor = 5456.32M
                },
                new HistoricoFinanca
                {
                    Id = "45647",
                    Descricao = "Sorvete",
                    Data = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                    Valor = 12.36M
                },
            });

            listagemHistoricoFinancasServicoMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public async Task Get_PorId_Sucesso()
        {
            //Preparação
            listagemHistoricoFinancasServicoMock.Setup(x => x.Listar("IdConta1")).ReturnsAsync(new List<HistoricoFinanca>
            {
                new HistoricoFinanca
                {
                    Id = "45647",
                    Descricao = "Sorvete",
                    Data = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                    Valor = 12.36M
                },
            });

            //Ação
            var resultado = await controller.Get("IdConta1") as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);

            resultado.Value.Should().BeEquivalentTo(new List<HistoricoFinanca>
            {
                new HistoricoFinanca
                {
                    Id = "45647",
                    Descricao = "Sorvete",
                    Data = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                    Valor = 12.36M
                },
            });

            listagemHistoricoFinancasServicoMock.Verify(x => x.Listar("IdConta1"), Times.Once);
        }
    }
}
