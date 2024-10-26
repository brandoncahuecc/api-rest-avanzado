using MediatR;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;
using rest_reporte.Servicios;

namespace rest_reporte.Mediadores
{
    public class ObtenerReporteRequest : IRequest<Respuesta<Reporte, Mensaje>>
    {
        public string TipoReporte { get; set; }
        public int Id { get; set; }
    }

    public class ObtenerReporteHandler : IRequestHandler<ObtenerReporteRequest, Respuesta<Reporte, Mensaje>>
    {
        private readonly IReporteServicio _reporte;

        public ObtenerReporteHandler(IReporteServicio reporte)
        {
            _reporte = reporte;
        }

        public async Task<Respuesta<Reporte, Mensaje>> Handle(ObtenerReporteRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _reporte.ObtenerReportes(request.TipoReporte, request.Id);
            return resultado;
        }
    }
}
