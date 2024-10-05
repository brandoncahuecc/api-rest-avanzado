using Clase03.Modelos;
using Clase03.Persistencia;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Clase03.Servicios
{
    public interface ICategoriaServicio
    {
        Task<List<Categoria>> ObtenerTodos();
        Task<Categoria> ObtenerUno(int id);
        Task<Categoria> Crear(Categoria request);
        Task<Categoria> Actualizar(Categoria request);
        Task<bool> Eliminar(int id);
    }

    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly ICategoriaPersistencia _categoriaPersistencia;

        public CategoriaServicio(ICategoriaPersistencia categoriaPersistencia)
        {
            _categoriaPersistencia = categoriaPersistencia;
        }

        public async Task<Categoria> Actualizar(Categoria request)
        {
            return await _categoriaPersistencia.Actualizar(request);
        }

        public async Task<Categoria> Crear(Categoria request)
        {
            return await _categoriaPersistencia.Crear(request);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _categoriaPersistencia.Eliminar(id);
        }

        public async Task<List<Categoria>> ObtenerTodos()
        {
            return await _categoriaPersistencia.ObtenerTodos();
        }

        public async Task<Categoria> ObtenerUno(int id)
        {
            return await _categoriaPersistencia.ObtenerUno(id);
        }
    }
}
