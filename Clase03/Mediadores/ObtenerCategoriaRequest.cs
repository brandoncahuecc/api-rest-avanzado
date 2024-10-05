using Clase03.Modelos;
using Clase03.Servicios;
using MediatR;

namespace Clase03.Mediadores
{
    public class ObtenerCategoriaRequest : IRequest<Categoria>
    {
        public int Id { get; set; }
    }

    public class ObtenerCategoriaHandler : IRequestHandler<ObtenerCategoriaRequest, Categoria>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public ObtenerCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<Categoria> Handle(ObtenerCategoriaRequest request, CancellationToken cancellationToken)
        {
            Categoria categoria = await _categoriaServicio.ObtenerUno(request.Id);
            return categoria;
        }
    }
}
