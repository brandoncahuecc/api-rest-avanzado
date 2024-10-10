using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rest_biblioteca.Modelos.Global;

namespace rest_biblioteca.Middlewares
{
    public class CustomeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeMiddleware> _logger;

        public CustomeMiddleware(RequestDelegate next, ILogger<CustomeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Solicitud API: " + context.Request.Method + " - " + context.Request.Path);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GENERAL EN API");

                Mensaje mensaje = new("", "Tuvimos problemas no controlados, contacte con el administrador", ex);

                var respuesta = context.Response;
                respuesta.StatusCode = 500;
                respuesta.ContentType = "application/json";
                await respuesta.WriteAsync(JsonConvert.SerializeObject(mensaje));
            }

            _logger.LogInformation("Respuesta API: " + context.Response.StatusCode);
        }
    }
}
