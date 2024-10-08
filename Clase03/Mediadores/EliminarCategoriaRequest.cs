using Clase03.Modelos.Global;
using Clase03.Servicios;
using MediatR;

namespace Clase03.Mediadores
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
