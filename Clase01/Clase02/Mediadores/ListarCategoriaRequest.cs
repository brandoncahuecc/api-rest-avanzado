using Clase02.Modelos;
using Clase02.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clase02.Mediadores
{
    public class ListarCategoriaRequest : IRequest<List<Categoria>>
    {
    }

    public class ListarCategoriaHandler : IRequestHandler<ListarCategoriaRequest, List<Categoria>>
    {
        private readonly ClaseDbContext _context;

        public ListarCategoriaHandler(ClaseDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
        {
            List<Categoria> categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }
    }
}
