using Clase02.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Clase02.Persistencia
{
    public class ClaseDbContext : DbContext
    {
        public ClaseDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
