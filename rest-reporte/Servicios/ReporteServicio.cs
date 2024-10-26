using DinkToPdf;
using DinkToPdf.Contracts;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace rest_reporte.Servicios
{
    public interface IReporteServicio
    {
        Task<Respuesta<Reporte, Mensaje>> ObtenerReportes(string tipoReporte, int id);
    }

    public class ReporteServicio : IReporteServicio
    {
        private IConverter _converter;

        public ReporteServicio(IConverter converter)
        {
            _converter = converter;
        }

        private string GenerarPdf(string html)
        {
            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = new PechkinPaperSize("5in", "10in"),
                    //PaperSize = PaperKind.Letter,
                    Orientation = Orientation.Portrait
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = html,
                        WebSettings =
                        {
                            DefaultEncoding = "utf-8",
                            LoadImages = true,
                            PrintMediaType = true,
                            EnableIntelligentShrinking = false
                        },
                        UseExternalLinks = true,
                        UseLocalLinks = true
                    }
                }
            };

            var documento = _converter.Convert(pdf);
            return Convert.ToBase64String(documento);
        }

        private async Task<string> ObtenerBase64Imagen(string url)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    byte[] imageByte = await cliente.GetByteArrayAsync(url);
                    string base64 = Convert.ToBase64String(imageByte);
                    return $"data:image/jpg;base64,{base64}";
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Respuesta<Reporte, Mensaje>> ObtenerReportes(string tipoReporte, int id)
        {
            Respuesta<Reporte, Mensaje> respuesta = new();

            switch (tipoReporte)
            {
                case "Compra":
                    {
                        string html = "<!DOCTYPE html><html lang=\"es\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Reporte de Ingresos</title> <style> body { font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #f4f4f4; } h1 { text-align: center; color: #333; } table { width: 100%; border-collapse: collapse; margin-bottom: 20px; } table, th, td { border: 1px solid #ddd; } th, td { padding: 10px; text-align: left; } th { background-color: #f2f2f2; } .summary { margin-bottom: 20px; } .summary p { margin: 5px 0; } </style></head><body> <img src=\"@LogoImg\" width=\"100\" height=\"100\"><h1>Reporte de Ingresos</h1> <div class=\"summary\"> <p><strong>Fecha del Reporte:</strong>@FechaReporte</p> <p><strong>Total de Ingresos:</strong>@TotalIngresos</p> </div> <table> <thead> <tr> <th>ID Ingreso</th> <th>Proveedor</th> <th>Fecha Hora</th> <th>Total Compra</th> <th>Forma Pago</th> <th>Estado</th> </tr> </thead> <tbody> @DetalleArticulos </tbody> </table></body></html>";
                        string htmlDetalleBase = "<tr> <td>@IdIngreso</td> <td>@IdArticulo</td> <td>@Cantidad</td> <td>@PrecioCompra</td> <td>@PrecioVenta</td> <td>@Stock</td> </tr>";
                        string logoUrl = "https://png.pngtree.com/png-clipart/20190516/original/pngtree-eagle-business-logo-design-creative-logo-design-concept-with-artistic-png-image_3623564.jpg";

                        string htmlDetalle = string.Empty;
                        for (int i = 0; i < 5; i++)
                        {
                            htmlDetalle += htmlDetalleBase;
                        }

                        string imagen = await ObtenerBase64Imagen(logoUrl);

                        html = html.Replace("@DetalleArticulos", htmlDetalle)
                            .Replace("@FechaReporte", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                            .Replace("@LogoImg", imagen);

                        string base64 = GenerarPdf(html);

                        return respuesta.RespuestaExito(new Reporte()
                        {
                            Nombre = $"Compra-{id}-{DateTime.Now.ToString("ddMMyyyy-HHmm")}",
                            Formato = "text/html",
                            Contenido = base64
                        });
                    }
                default:
                    return respuesta.RespuestaError(501, new("NOT-IMPLEM", $"El reporte {tipoReporte} no esta implementado"));
            }
        }
    }
}
