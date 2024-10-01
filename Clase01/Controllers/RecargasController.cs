using Clase01.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clase01.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class RecargasController : ControllerBase
    {
        private List<Paquete> paquetes = new List<Paquete>()
        {
            new Paquete(){ Descripcion = "Paquete Basico", Precio = 10 },
            new Paquete(){ Descripcion = "Paquete Medio", Precio = 20 },
            new Paquete(){ Descripcion = "Paquete Premium", Precio = 50 }
        };

        [HttpPost]
        public IActionResult Recargar(Recarga recarga)
        {
            if (recarga.Numero.Equals("12345678") && recarga.Nit.Equals("25"))
                return Ok(new { Mensaje = "Tu recarga se realizó con éxico", Valor = 100.00 });

            return BadRequest(new { Mensaje = "Numero o nit incorrecto" });
        }

        [HttpGet]
        public IActionResult ObtenerPaquetes()
        {
            return Ok(paquetes);
        }
    }
}
