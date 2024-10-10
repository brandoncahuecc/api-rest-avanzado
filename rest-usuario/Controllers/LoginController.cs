using MediatR;
using Microsoft.AspNetCore.Mvc;
using rest_biblioteca.Controllers;
using rest_usuario.Mediadores;

namespace rest_usuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(IniciarSesionRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return RespuestaPerzonalizada(respuesta);
        }

        [HttpPost("Refrescar")]
        public async Task<IActionResult> RefrescarToken(RefrescarTokenRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return RespuestaPerzonalizada(respuesta);
        }
    }
}
