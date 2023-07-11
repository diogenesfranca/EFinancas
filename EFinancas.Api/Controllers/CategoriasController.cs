using Microsoft.AspNetCore.Mvc;

namespace EFinancas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> logger;

        public CategoriasController(ILogger<CategoriasController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            
        }
    }
}