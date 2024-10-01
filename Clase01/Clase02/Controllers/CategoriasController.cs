using Clase02.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clase02.Controllers
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
        public IActionResult Get(int id)
        {
            //var respuesta = _mediator.Send();
            //return Ok(respuesta);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearCategoriaRequest request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public IActionResult Post(int id)
        {
            //var respuesta = _mediator.Send();
            //return Ok(respuesta);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var respuesta = _mediator.Send();
            //return Ok(respuesta);
            return Ok();
        }
    }
}
