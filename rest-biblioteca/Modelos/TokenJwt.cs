using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_biblioteca.Modelos
{
    public class TokenJwt
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
