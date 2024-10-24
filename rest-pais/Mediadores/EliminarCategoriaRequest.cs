using rest_pais.Servicios;
using MediatR;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace rest_pais.Mediadores
{
    public class EliminarCategoriaRequest : IRequest<Respuesta<Mensaje, Mensaje>>
    {
        public int Id { get; set; }
    }

    public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaRequest, Respuesta<Mensaje, Mensaje>>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public EliminarCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<Respuesta<Mensaje, Mensaje>> Handle(EliminarCategoriaRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _categoriaServicio.Eliminar(request.Id);
            return resultado;
        }
    }
}
