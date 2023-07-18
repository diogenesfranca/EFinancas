using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EFinancas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> logger;
        private readonly ICategoriasRepositorio categoriasRepositorio;

        public CategoriasController(ILogger<CategoriasController> logger, ICategoriasRepositorio categoriasRepositorio)
        {
            this.logger = logger;
            this.categoriasRepositorio = categoriasRepositorio;
        }

        [HttpGet]
        public void Get()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(string categoria)
        {
            await categoriasRepositorio.Inserir(new Categoria { Descricao = categoria });
            return Ok();
        }
    }
}