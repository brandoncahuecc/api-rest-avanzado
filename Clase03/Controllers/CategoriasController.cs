using Clase03.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rest_biblioteca.Controllers;

namespace Clase03.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get()
        {
            var respuesta = await _mediator.Send(new ListarCategoriaRequest());
            return RespuestaPerzonalizada(respuesta);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get(int id)
        {
            var respuesta = await _mediator.Send(new ObtenerCategoriaRequest() { Id = id });
            return RespuestaPerzonalizada(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearCategoriaRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return RespuestaPerzonalizada(respuesta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ActualizarCategoriaRequest request)
        {
            request.IdCategoria = id;
            var respuesta = await _mediator.Send(request);
            return RespuestaPerzonalizada(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var respuesta = await _mediator.Send(new EliminarCategoriaRequest() { Id = id });
            return RespuestaPerzonalizada(respuesta);
        }
    }
}
