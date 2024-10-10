using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace rest_biblioteca.Modelos.Global
{
    public class Respuesta<TExito, TMensaje>
    {
        public bool EsExitoso { get; set; }
        public int CodigoEstado { get; set; }
        public TExito Objeto { get; set; }
        public TMensaje Mensaje { get; set; }

        public Respuesta<TExito, TMensaje> RespuestaExito(TExito exito)
        {
            return new Respuesta<TExito, TMensaje>
            {
                EsExitoso = true,
                CodigoEstado = 200,
                Objeto = exito
            };
        }

        public Respuesta<TExito, TMensaje> RespuestaError(int codigoEstado, TMensaje mensaje)
        {
            return new Respuesta<TExito, TMensaje>
            {
                EsExitoso = false,
                CodigoEstado = codigoEstado,
                Mensaje = mensaje
            };
        }
    }
}
