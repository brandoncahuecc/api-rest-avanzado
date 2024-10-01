using Clase02.Modelos;
using Clase02.Persistencia;
using MediatR;

namespace Clase02.Mediadores
{
    public class CrearCategoriaRequest : IRequest<Categoria>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaRequest, Categoria>
    {
        private readonly ClaseDbContext _context;

        public CrearCategoriaHandler(ClaseDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> Handle(CrearCategoriaRequest request, CancellationToken cancellationToken)
        {
            Categoria categoria = new()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Estado = true
            };

            await _context.AddAsync(categoria);
            var cantidadRegistrado = await _context.SaveChangesAsync();

            if (cantidadRegistrado > 0)
                return categoria;
            else
                return null;
        }
    }
}
