using Clase01.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clase01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> clientes = new List<Cliente>()
        {
            new(){ Documento= "101", Nombre = "Cliente", Apellido = "Uno", Edad = 20 },
            new(){ Documento= "102", Nombre = "Cliente", Apellido = "Dos", Edad = 20 },
            new(){ Documento= "103", Nombre = "Cliente", Apellido = "Tres", Edad = 20 }
        };

        private static List<Cliente> clientesDos = new List<Cliente>()
        {
            new(){ Documento= "104", Nombre = "Cliente", Apellido = "Cuatro", Edad = 20 },
            new(){ Documento= "105", Nombre = "Cliente", Apellido = "Cinco", Edad = 20 },
            new(){ Documento= "106", Nombre = "Cliente", Apellido = "Seis", Edad = 20 }
        };

        [HttpGet]
        public IActionResult ObtenerClientes()
        {
            return Ok(clientes);
        }

        [HttpGet]
        [Route("Dos")]
        public IActionResult ObtenerClientesDos()
        {
            return Ok(clientesDos);
        }

        [HttpGet]
        [Route("Tres")]
        public IActionResult ObtenerClientesasdfasd()
        {
            return Ok(clientesDos);
        }

        [HttpDelete("{documento}")]
        public IActionResult EliminarCliente(string documento)
        {
            Cliente cliente = clientes.FirstOrDefault(c => c.Documento.Equals(documento));

            if (cliente == null)
                return BadRequest(new { Mensaje = "Cliente no existe para eliminarlo" });

            clientes.Remove(cliente);

            return Ok(new { Mensaje = "Cliente eliminado exitosamente" });
        }

    }
}
