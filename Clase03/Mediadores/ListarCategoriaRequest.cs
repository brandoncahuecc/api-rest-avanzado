using Clase03.Modelos;
using Clase03.Servicios;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Clase03.Mediadores
{
    public class ListarCategoriaRequest : IRequest<List<Categoria>>
    {
    }

    public class ListarCategoriaHandler : IRequestHandler<ListarCategoriaRequest, List<Categoria>>
    {
        private readonly ICategoriaServicio _categoriaServicio;
        private readonly IDistributedCache _distributedCache;

        public ListarCategoriaHandler(ICategoriaServicio categoriaServicio, IDistributedCache distributedCache)
        {
            _categoriaServicio = categoriaServicio;
            _distributedCache = distributedCache;
        }

        public async Task<List<Categoria>> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
        {
            //Verificar si hay cache de categorias
            List<Categoria> categorias = new();
            try
            {
                string categoriasCache = await _distributedCache.GetStringAsync("Categorias") ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(categoriasCache))
                {
                    categorias = JsonConvert.DeserializeObject<List<Categoria>>(categoriasCache);
                    return categorias;
                }
            }
            catch (Exception ex)
            {

            }

            //Si no hay datos en cache o fallo la conexión vamos a traer a BD los registros
            categorias = await _categoriaServicio.ObtenerTodos();
            await Task.Delay(3000);
            //Guardamos los datos de BD en Cache
            try
            {
                if (categorias is not null)
                {
                    TimeSpan time = TimeSpan.FromSeconds(20);
                    DistributedCacheEntryOptions options = new();
                    options.SetAbsoluteExpiration(time);

                    await _distributedCache.SetStringAsync("Categorias", JsonConvert.SerializeObject(categorias), options);
                }
            }
            catch (Exception ex)
            {

            }

            return categorias;
        }
    }
}
