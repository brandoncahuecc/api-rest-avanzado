using Newtonsoft.Json;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;
using System.Net.Http.Headers;
using System.Text;

namespace rest_reporte.Servicios
{
    public interface ICategoriaCliente
    {
        Task<Respuesta<List<Categoria>, Mensaje>> ObtenerCategorias();
    }

    public class CategoriaCliente : ICategoriaCliente
    {
        private readonly IHttpClientFactory _factory;

        public CategoriaCliente(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        private async Task<Respuesta<TokenJwt, Mensaje>> ObtenerToken()
        {
            var cliente = _factory.CreateClient("ClienteCategoria");
            string url = "Login";

            var credenciales = new
            {
                Usuario = "admin",
                Clave = "e8f9a650174f078d84b0ea9f7609deaf0ffae0ce66ad2adbf980391e8f4d9273"
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            HttpResponseMessage response = await cliente.SendAsync(request);

            Respuesta<TokenJwt, Mensaje> respuesta = new();

            if (response.IsSuccessStatusCode)
            {
                string contenido = await response.Content.ReadAsStringAsync();
                TokenJwt token = JsonConvert.DeserializeObject<TokenJwt>(contenido);
                return respuesta.RespuestaExito(token);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                string contenido = await response.Content.ReadAsStringAsync();
                Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(contenido);
                return respuesta.RespuestaError(401, mensaje);
            }
            else
            {
                return respuesta.RespuestaError(400, new Mensaje("NO-DATA", "No se logro autenticar para obtener las categorias"));
            }
        }

        public async Task<Respuesta<List<Categoria>, Mensaje>> ObtenerCategorias()
        {
            Respuesta<TokenJwt, Mensaje> respuestaToken = await ObtenerToken();
            Respuesta<List<Categoria>, Mensaje> respuesta = new();

            if (!respuestaToken.EsExitoso)
                return respuesta.RespuestaError(respuestaToken.CodigoEstado, respuestaToken.Mensaje);

            TokenJwt token = respuestaToken.Objeto;

            var cliente = _factory.CreateClient("ClienteCategoria");
            string url = "Categorio";
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            HttpResponseMessage response = await cliente.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string contenido = await response.Content.ReadAsStringAsync();
                List<Categoria> categorias = JsonConvert.DeserializeObject<List<Categoria>>(contenido);
                return respuesta.RespuestaExito(categorias);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return respuesta.RespuestaError(401, new Mensaje("NO-AUTH", "No se logro autenticar para obtener las categorias"));
            }
            else
            {
                return respuesta.RespuestaError(400, new Mensaje("NO-DATA", "No se logro obtener las categorias"));
            }
        }
    }
}
