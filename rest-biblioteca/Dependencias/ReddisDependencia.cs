using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_biblioteca.Dependencias
{
    public static class ReddisDependencia
    {
        public static IServiceCollection AgregarReddisCache(this IServiceCollection service)
        {
            return service.AddStackExchangeRedisCache(option =>
            {
                string redisConnection = Environment.GetEnvironmentVariable("RedisConnection") ?? string.Empty;
                option.Configuration = redisConnection;
            });
        }
    }
}
