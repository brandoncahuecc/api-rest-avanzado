using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace con_cliente_web_service.Modelos
{
    public class TokenJwt
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
