using EFinancas.Api.Controllers;
using EFinancas.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFinancas.Testes.Api.Controllers
{
    public class HistoricoFinancasControllerTestes
    {
        private readonly Mock<IListagemHistoricoFinancasServico> listagemHistoricoFinancasServicoMock = new();

        private readonly HistoricoFinancasController controller;

        public HistoricoFinancasControllerTestes()
        {
            controller = new HistoricoFinancasController(listagemHistoricoFinancasServicoMock.Object);
        }

        [Fact]
        public async Task Get_Sucesso()
        {
            //Ação
            var resultado = await controller.Get() as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);
            listagemHistoricoFinancasServicoMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public async Task Get_PorId_Sucesso()
        {
            //Ação
            var resultado = await controller.Get("IdConta1") as OkObjectResult;

            //Afirmação
            Assert.Equal((int)HttpStatusCode.OK, resultado!.StatusCode);
            listagemHistoricoFinancasServicoMock.Verify(x => x.Listar("IdConta1"), Times.Once);
        }
    }
}
