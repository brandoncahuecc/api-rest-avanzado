using Clase03.Servicios;
using MediatR;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace Clase03.Mediadores
{
    public class ObtenerCategoriaRequest : IRequest<Respuesta<Categoria, Mensaje>>
    {
        public int Id { get; set; }
    }

    public class ObtenerCategoriaHandler : IRequestHandler<ObtenerCategoriaRequest, Respuesta<Categoria, Mensaje>>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public ObtenerCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<Respuesta<Categoria, Mensaje>> Handle(ObtenerCategoriaRequest request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaServicio.ObtenerUno(request.Id);
            return categoria;
        }
    }
}
