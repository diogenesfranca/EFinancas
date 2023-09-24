using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace EFinancas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/receitas")]
    public class ReceitasController : ControllerBase
    {
        private readonly ILogger<ReceitasController> logger;
        private readonly IReceitasRepositorio receitasRepositorio;
        private readonly IGerenciamentoReceitasServico gerenciamentoReceitasServico;

        public ReceitasController(ILogger<ReceitasController> logger, IReceitasRepositorio receitasRepositorio, IGerenciamentoReceitasServico gerenciamentoReceitasServico)
        {
            this.logger = logger;
            this.receitasRepositorio = receitasRepositorio;
            this.gerenciamentoReceitasServico = gerenciamentoReceitasServico;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!ObjectId.TryParse(id, out var _))
                return BadRequest("Id inválido, por favor forneça um id no formato correto de 24 caracteres hexadecimais.");

            var receita = await receitasRepositorio.Obter(id);

            if (receita == null)
                return NotFound("Receita não encontrada.");

            return Ok(receita);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await receitasRepositorio.Listar());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Receita receita)
        {
            try
            {
                await gerenciamentoReceitasServico.Inserir(receita);
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
        public async Task<IActionResult> Put(string id, [FromBody] Receita receita)
        {
            try
            {
                await gerenciamentoReceitasServico.Atualizar(id, receita);
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
            await receitasRepositorio.Deletar(id);
            return Ok();
        }
    }
}
