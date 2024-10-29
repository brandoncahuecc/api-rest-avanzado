using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace con_cliente_web_service
{
    public class ApiServiceConnection
    {
        private string _apiUrl;

        public ApiServiceConnection()
        {
            _apiUrl = Environment.GetEnvironmentVariable("ApiServiceUrl") ?? string.Empty;
        }

        public async Task<Tuple<T, TOut>> ExecuteGenericMethod<TIn, T, TOut>(string method, TIn content, TypeMethod typeOfMethod, string token = null)
        {
            if (string.IsNullOrWhiteSpace(_apiUrl)) throw new Exception("No se encontró la URL del API.");

            try
            {
                using (var cliente = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    StringContent stringContent = null;
                    if (content != null)
                        stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                    HttpResponseMessage respuesta;

                    switch (typeOfMethod)
                    {
                        case TypeMethod.Get:
                            respuesta = await cliente.GetAsync(string.Format("{0}{1}", _apiUrl, method));
                            break;
                        case TypeMethod.Post:
                            respuesta = await cliente.PostAsync(string.Format("{0}{1}", _apiUrl, method), stringContent);
                            break;
                        case TypeMethod.Put:
                            respuesta = await cliente.PutAsync(string.Format("{0}{1}", _apiUrl, method), stringContent);
                            break;
                        case TypeMethod.Delete:
                            respuesta = await cliente.DeleteAsync(string.Format("{0}{1}", _apiUrl, method));
                            break;
                        default:
                            throw new Exception("Método no implementado.");
                    }

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string responseContent = await respuesta.Content.ReadAsStringAsync();
                        try
                        {
                            T responseData = JsonConvert.DeserializeObject<T>(responseContent);
                            return new Tuple<T, TOut>(responseData, default(TOut));
                        }
                        catch
                        {
                            TOut response = JsonConvert.DeserializeObject<TOut>(responseContent);
                            return new Tuple<T, TOut>(default(T), response);
                        }
                    }
                    else
                    {
                        string responseContent = await respuesta.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);

                        TOut errorResponse = JsonConvert.DeserializeObject<TOut>(responseContent);

                        return new Tuple<T, TOut>(default(T), errorResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public enum TypeMethod
    {
        Get,
        Post,
        Put,
        Delete
    }
}
