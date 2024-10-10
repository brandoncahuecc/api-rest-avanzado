using Clase03.Servicios;
using MediatR;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace Clase03.Mediadores
{
    public class CrearCategoriaRequest : IRequest<Respuesta<Categoria, Mensaje>>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaRequest, Respuesta<Categoria, Mensaje>>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public CrearCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<Respuesta<Categoria, Mensaje>> Handle(CrearCategoriaRequest request, CancellationToken cancellationToken)
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
