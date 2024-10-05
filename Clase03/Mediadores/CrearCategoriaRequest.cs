using Clase03.Modelos;
using Clase03.Servicios;
using MediatR;

namespace Clase03.Mediadores
{
    public class CrearCategoriaRequest : IRequest<Categoria>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaRequest, Categoria>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public CrearCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<Categoria> Handle(CrearCategoriaRequest request, CancellationToken cancellationToken)
        {
            Categoria categoria = new()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Condicion = 1
            };

            var resultado = await _categoriaServicio.Crear(categoria);
            return resultado;
        }
    }
}
