using Microsoft.Extensions.DependencyInjection;

namespace rest_biblioteca.Dependencias
{
    public static class MediadorDependencia
    {
        public static IServiceCollection RegistrarMediador<T>(this IServiceCollection service)
        {
            return service.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(T).Assembly));
        }
    }
}
