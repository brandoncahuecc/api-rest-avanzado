using Clase03.Modelos.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clase03.Controllers
{
    public class CustomeControllerBase : ControllerBase
    {
        public IActionResult RespuestaPerzonalizada<TExito, TMensaje>(Respuesta<TExito, TMensaje> respuesta)
        {
            return StatusCode(respuesta.CodigoEstado, respuesta.EsExitoso ? respuesta.Objeto : respuesta.Mensaje);
        }
    }
}
