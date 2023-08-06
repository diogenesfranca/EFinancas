using EFinancas.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace EFinancas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/historico-financas")]
    public class HistoricoFinancasController : ControllerBase
    {
        private readonly IListagemHistoricoFinancasServico listagemHistoricoFinancasServico;

        public HistoricoFinancasController(IListagemHistoricoFinancasServico listagemHistoricoFinancasServico)
        {
            this.listagemHistoricoFinancasServico = listagemHistoricoFinancasServico;
        }

        /// <summary>
        /// Método responsável por listar as receitas e despesas ordenadas por data de criação
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await listagemHistoricoFinancasServico.Listar());
        }

        /// <summary>
        /// Método responsável por listar as receitas e despesas de uma conta ordenadas por data de criação
        /// </summary>
        [HttpGet("conta/{idConta}")]
        public async Task<IActionResult> Get(string idConta)
        {
            return Ok(await listagemHistoricoFinancasServico.Listar(idConta));
        }
    }
}