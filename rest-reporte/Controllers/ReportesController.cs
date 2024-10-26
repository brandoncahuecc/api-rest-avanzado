using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rest_biblioteca.Controllers;
using rest_reporte.Mediadores;

namespace rest_reporte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tipoReporte}/{id}")]
        public async Task<IActionResult> ObtenerReportes(string tipoReporte, int id)
        {
            var respuesta = await _mediator.Send(new ObtenerReporteRequest() { TipoReporte = tipoReporte, Id = id });
            return RespuestaPerzonalizada(respuesta);
        }
    }
}
