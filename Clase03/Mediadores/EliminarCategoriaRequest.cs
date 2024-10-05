using Clase03.Servicios;
using MediatR;

namespace Clase03.Mediadores
{
    public class EliminarCategoriaRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaRequest, bool>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public EliminarCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<bool> Handle(EliminarCategoriaRequest request, CancellationToken cancellationToken)
        {
            bool resultado = await _categoriaServicio.Eliminar(request.Id);
            return resultado;
        }
    }
}
