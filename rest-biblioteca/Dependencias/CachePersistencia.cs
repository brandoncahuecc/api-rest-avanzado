using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace rest_biblioteca.Dependencias
{
    public interface ICachePersistencia
    {
        Task<T> GetCache<T>(string key);
        Task SetCache<T>(string key, T objeto);
    }

    public class CachePersistencia : ICachePersistencia
    {
        private readonly IDistributedCache _distributedCache;

        public CachePersistencia(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetCache<T>(string key)
        {
            try
            {
                string categoriasCache = await _distributedCache.GetStringAsync(key) ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(categoriasCache))
                    return JsonConvert.DeserializeObject<T>(categoriasCache);
                return default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public async Task SetCache<T>(string key, T objeto)
        {
            try
            {
                if (objeto is not null)
                {
                    TimeSpan time = TimeSpan.FromSeconds(20);
                    DistributedCacheEntryOptions options = new();
                    options.SetAbsoluteExpiration(time);

                    await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(objeto), options);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
