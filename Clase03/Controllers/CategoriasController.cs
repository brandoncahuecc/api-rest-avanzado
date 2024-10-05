using Clase03.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clase03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var respuesta = await _mediator.Send(new ListarCategoriaRequest());
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var respuesta = await _mediator.Send(new ObtenerCategoriaRequest() { Id = id });
            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearCategoriaRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ActualizarCategoriaRequest request)
        {
            request.IdCategoria = id;
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var respuesta = await _mediator.Send(new EliminarCategoriaRequest() { Id = id });
            return Ok(respuesta);
        }
    }
}
