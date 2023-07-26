using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFinancas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/contas")]
    public class ContasController : ControllerBase
    {
        private readonly ILogger<ContasController> logger;
        private readonly IContasRepositorio contasRepositorio;
        private readonly IGerenciamentoContasServico gerenciamentoContasServico;

        public ContasController(ILogger<ContasController> logger, IContasRepositorio contasRepositorio, IGerenciamentoContasServico gerenciamentoContasServico)
        {
            this.logger = logger;
            this.contasRepositorio = contasRepositorio;
            this.gerenciamentoContasServico = gerenciamentoContasServico;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await contasRepositorio.Listar());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Conta conta)
        {
            try
            {
                await gerenciamentoContasServico.Inserir(conta);
                return Ok();
            }
            catch (ContaException ex)
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
        public async Task<IActionResult> Put(string id, [FromBody] Conta conta)
        {
            try
            {
                await gerenciamentoContasServico.Atualizar(id, conta);
                return Ok();
            }
            catch (ContaException ex)
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
            await contasRepositorio.Deletar(id);
            return Ok();
        }
    }
}
