using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFinancas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/despesas")]
    public class DespesasController : ControllerBase
    {
        private readonly ILogger<DespesasController> logger;
        private readonly IDespesasRepositorio despesasRepositorio;
        private readonly IGerenciamentoDespesasServico gerenciamentoDespesasServico;

        public DespesasController(ILogger<DespesasController> logger, IDespesasRepositorio despesasRepositorio, IGerenciamentoDespesasServico gerenciamentoDespesasServico)
        {
            this.logger = logger;
            this.despesasRepositorio = despesasRepositorio;
            this.gerenciamentoDespesasServico = gerenciamentoDespesasServico;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await despesasRepositorio.Listar());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Despesa despesa)
        {
            try
            {
                await gerenciamentoDespesasServico.Inserir(despesa);
                return Ok();
            }
            catch (FinancaException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Despesa despesa)
        {
            try
            {
                await gerenciamentoDespesasServico.Atualizar(id, despesa);
                return Ok();
            }
            catch (FinancaException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await despesasRepositorio.Deletar(id);
            return Ok();
        }
    }
}
