using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rest_biblioteca.Modelos;

namespace rest_catalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private static readonly List<Catalogo> catalogos = new List<Catalogo>()
        {
            new Catalogo(){ Codigo= "1", Descripcion = "Uno"},
            new Catalogo(){ Codigo= "2", Descripcion = "Dos"},
            new Catalogo(){ Codigo= "3", Descripcion = "Tres"},
            new Catalogo(){ Codigo= "4", Descripcion = "Cuatro"},
            new Catalogo(){ Codigo= "5", Descripcion = "Cinco"},
            new Catalogo(){ Codigo= "6", Descripcion = "Seis"},
        };

        public CatalogosController()
        {

        }

        [HttpGet]
        public IActionResult Obtener()
        {
            return Ok(catalogos);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerUno(string id)
        {
            return Ok(catalogos.FirstOrDefault(c => c.Codigo.Equals(id)));
        }
    }
}
