using Clase03.Modelos;
using Clase03.Modelos.Global;
using Clase03.Persistencia;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Clase03.Servicios
{
    public interface ICategoriaServicio
    {
        Task<Respuesta<List<Categoria>, Mensaje>> ObtenerTodos();
        Task<Respuesta<Categoria, Mensaje>> ObtenerUno(int id);
        Task<Respuesta<Categoria, Mensaje>> Crear(Categoria request);
        Task<Respuesta<Categoria, Mensaje>> Actualizar(Categoria request);
        Task<Respuesta<Mensaje, Mensaje>> Eliminar(int id);
    }

    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly ICategoriaPersistencia _categoriaPersistencia;

        public CategoriaServicio(ICategoriaPersistencia categoriaPersistencia)
        {
            _categoriaPersistencia = categoriaPersistencia;
        }

        public async Task<Respuesta<Categoria, Mensaje>> Actualizar(Categoria request)
        {
            return await _categoriaPersistencia.Actualizar(request);
        }

        public async Task<Respuesta<Categoria, Mensaje>> Crear(Categoria request)
        {
            return await _categoriaPersistencia.Crear(request);
        }

        public async Task<Respuesta<Mensaje, Mensaje>> Eliminar(int id)
        {
            return await _categoriaPersistencia.Eliminar(id);
        }

        public async Task<Respuesta<List<Categoria>, Mensaje>> ObtenerTodos()
        {
            return await _categoriaPersistencia.ObtenerTodos();
        }

        public async Task<Respuesta<Categoria, Mensaje>> ObtenerUno(int id)
        {
            return await _categoriaPersistencia.ObtenerUno(id);
        }
    }
}
