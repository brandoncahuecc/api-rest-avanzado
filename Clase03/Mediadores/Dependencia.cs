namespace Clase03.Mediadores
{
    public static class Dependencia
    {
        public static IServiceCollection RegistrarMediador(this IServiceCollection service)
        {
            return service.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Dependencia).Assembly));
        }
    }
}
