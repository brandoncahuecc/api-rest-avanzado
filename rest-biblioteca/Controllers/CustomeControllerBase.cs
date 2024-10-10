using Microsoft.AspNetCore.Mvc;
using rest_biblioteca.Modelos.Global;

namespace rest_biblioteca.Controllers
{
    public class CustomeControllerBase : ControllerBase
    {
        public IActionResult RespuestaPerzonalizada<TExito, TMensaje>(Respuesta<TExito, TMensaje> respuesta)
        {
            return StatusCode(respuesta.CodigoEstado, respuesta.EsExitoso ? respuesta.Objeto : respuesta.Mensaje);
        }
    }
}
