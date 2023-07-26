using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFinancas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> logger;
        private readonly ICategoriasRepositorio categoriasRepositorio;
        private readonly IGerenciamentoCategoriasServico gerenciamentoCategoriasServico;

        public CategoriasController(ILogger<CategoriasController> logger, ICategoriasRepositorio categoriasRepositorio, IGerenciamentoCategoriasServico gerenciamentoCategoriasServico)
        {
            this.logger = logger;
            this.categoriasRepositorio = categoriasRepositorio;
            this.gerenciamentoCategoriasServico = gerenciamentoCategoriasServico;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await categoriasRepositorio.Listar());
        }

        [HttpPost]
        public async Task<IActionResult> Post(string categoria)
        {
            try
            {
                await gerenciamentoCategoriasServico.Inserir(categoria);
                return Ok();
            }
            catch (CategoriaException ex)
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
        public async Task<IActionResult> Put(string id, string categoria)
        {
            try
            {
                await gerenciamentoCategoriasServico.Atualizar(id, categoria);
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
            await categoriasRepositorio.Deletar(id);
            return Ok();
        }
    }
}