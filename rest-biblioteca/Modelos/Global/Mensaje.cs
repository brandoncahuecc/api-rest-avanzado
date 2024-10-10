using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_biblioteca.Modelos.Global
{
    public class Mensaje
    {
        public string CodigoInterno { get; set; }
        public string MensajeUsuario { get; set; }
        public string InformacionTecnica { get; set; }

        public Mensaje(string codigoInterno, string mensajeUsuario)
        {
            CodigoInterno = codigoInterno;
            MensajeUsuario = mensajeUsuario;
        }

        public Mensaje(string codigoInterno, string mensajeUsuario, Exception exception)
        {
            CodigoInterno = codigoInterno;
            MensajeUsuario = mensajeUsuario;
            InformacionTecnica = exception.Message;
        }
    }
}
