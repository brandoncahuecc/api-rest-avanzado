using Clase03.Servicios;
using MediatR;
using rest_biblioteca.Dependencias;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace Clase03.Mediadores
{
    public class ListarCategoriaRequest : IRequest<Respuesta<List<Categoria>, Mensaje>>
    {
    }

    public class ListarCategoriaHandler : IRequestHandler<ListarCategoriaRequest, Respuesta<List<Categoria>, Mensaje>>
    {
        private readonly ICategoriaServicio _categoriaServicio;
        private readonly ICachePersistencia _cachePersistencia;
        private readonly ILogger<ListarCategoriaHandler> _logger;

        public ListarCategoriaHandler(ICategoriaServicio categoriaServicio, ICachePersistencia cachePersistencia,
            ILogger<ListarCategoriaHandler> logger)
        {
            _categoriaServicio = categoriaServicio;
            _cachePersistencia = cachePersistencia;
            _logger = logger;
        }

        public async Task<Respuesta<List<Categoria>, Mensaje>> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Incializando variables");
            List<Categoria> categorias = new();
            var respuesta = new Respuesta<List<Categoria>, Mensaje>();

            _logger.LogError("Obteniendo cache de Redis");
            //Verificar si hay cache de categorias
            categorias = await _cachePersistencia.GetCache<List<Categoria>>("Categoria");

            _logger.LogWarning("Validando si hay datos en cache de Redis");
            if (categorias is not null && categorias.Count > 0)
                return respuesta.RespuestaExito(categorias);

            _logger.LogDebug("Obteniendo datos de BD");
            //Si no hay datos en cache o fallo la conexión vamos a traer a BD los registros
            respuesta = await _categoriaServicio.ObtenerTodos();

            _logger.LogCritical("Seteando datos en cache de Redis");
            //Guardamos los datos de BD en Cache
            if (respuesta.EsExitoso)
                await _cachePersistencia.SetCache("Categoria", respuesta.Objeto);
            
            return respuesta;
        }
    }
}
