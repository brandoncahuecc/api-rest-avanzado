using DinkToPdf;
using DinkToPdf.Contracts;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;
using rest_reporte.Recursos;

namespace rest_reporte.Servicios
{
    public interface IReporteServicio
    {
        Task<Respuesta<Reporte, Mensaje>> ObtenerReportes(string tipoReporte, int id);
    }

    public class ReporteServicio : IReporteServicio
    {
        private readonly IConverter _converter;
        private readonly ICategoriaCliente _categoria;

        public ReporteServicio(IConverter converter, ICategoriaCliente categoria)
        {
            _converter = converter;
            _categoria = categoria;
        }

        private string GenerarPdf(string html)
        {
            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    //PaperSize = new PechkinPaperSize("5in", "10in"),
                    PaperSize = PaperKind.Letter,
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
                        string html = Plantillas.HtmlCompra;
                        string htmlDetalleBase = "<tr> <td>@IdCategoria</td> <td>@Nombre</td> <td>@Descripcion</td> <td>@Condicion</td> </tr>";
                        string logoUrl = "https://png.pngtree.com/png-clipart/20190516/original/pngtree-eagle-business-logo-design-creative-logo-design-concept-with-artistic-png-image_3623564.jpg";

                        var categorias = await _categoria.ObtenerCategorias();

                        if (!categorias.EsExitoso)
                            return respuesta.RespuestaError(categorias.CodigoEstado, categorias.Mensaje);

                        List<Categoria> categoriasResult = categorias.Objeto;
                        string htmlDetalle = string.Empty;

                        categoriasResult.ForEach(categoria =>
                        {
                            htmlDetalle += htmlDetalleBase
                            .Replace("@IdCategoria", categoria.IdCategoria.ToString())
                            .Replace("@Nombre", categoria.Nombre)
                            .Replace("@Descripcion", categoria.Descripcion)
                            .Replace("@Condicion", categoria.Condicion == 1 ? "Activo" : "Inactivo");
                        });

                        //for (int i = 0; i < 5; i++)
                        //{
                        //    htmlDetalle += htmlDetalleBase;
                        //}

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
