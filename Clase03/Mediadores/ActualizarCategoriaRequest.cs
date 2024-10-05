using Clase03.Modelos;
using Clase03.Servicios;
using MediatR;

namespace Clase03.Mediadores
{
    public class ActualizarCategoriaRequest : IRequest<Categoria>
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaRequest, Categoria>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public ActualizarCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<Categoria> Handle(ActualizarCategoriaRequest request, CancellationToken cancellationToken)
        {
            Categoria categoria = new()
            {
                IdCategoria = request.IdCategoria,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };

            var resultado = await _categoriaServicio.Actualizar(categoria);
            return resultado;
        }

    }
}
