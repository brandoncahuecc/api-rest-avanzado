using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;
using rest_usuario.Persistencia;

namespace rest_usuario.Servicios
{
    public interface IUsuarioServicio
    {
        Task<Respuesta<TokenJwt, Mensaje>> IniciarSesion(string usuario, string clave);
        Task<Respuesta<TokenJwt, Mensaje>> RefrescarToken(TokenJwt request);
    }

    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioPersistencia _persistencia;
        private readonly IGeneradorTokenJwt _generadorTokenJwt;

        public UsuarioServicio(IUsuarioPersistencia persistencia, IGeneradorTokenJwt generadorTokenJwt)
        {
            _persistencia = persistencia;
            _generadorTokenJwt = generadorTokenJwt;
        }

        public async Task<Respuesta<TokenJwt, Mensaje>> IniciarSesion(string usuario, string clave)
        {
            var respuestaBusqueda = await _persistencia.BuscarUsuarioPorLogin(usuario);

            Respuesta<TokenJwt, Mensaje> respuesta = new();

            if (!respuestaBusqueda.EsExitoso)
                return respuesta.RespuestaError(401, respuestaBusqueda.Mensaje);

            Usuario usuarioTemp = respuestaBusqueda.Objeto;

            if (!clave.Equals(usuarioTemp.Clave))
                return respuesta.RespuestaError(401, new Mensaje("NO-PASS-VALID", "Usuario o contraseña  incorrecta"));

            string tokenStr = _generadorTokenJwt.GenerarTokenAcceso(usuarioTemp);

            TokenJwt token = new()
            {
               Token = tokenStr,
               RefreshToken = string.Empty
            };

            return respuesta.RespuestaExito(token);
        }

        public async Task<Respuesta<TokenJwt, Mensaje>> RefrescarToken(TokenJwt request)
        {
            throw new NotImplementedException();
        }
    }
}
