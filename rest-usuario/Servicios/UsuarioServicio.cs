using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;
using rest_usuario.Persistencia;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var respuestaBusqueda = await _persistencia.BuscarUsuarioPorLogin(usuario, 0);

            Respuesta<TokenJwt, Mensaje> respuesta = new();

            if (!respuestaBusqueda.EsExitoso)
                return respuesta.RespuestaError(401, respuestaBusqueda.Mensaje);

            Usuario usuarioTemp = respuestaBusqueda.Objeto;

            if (!clave.Equals(usuarioTemp.Clave))
                return respuesta.RespuestaError(401, new Mensaje("NO-PASS-VALID", "Usuario o contraseña  incorrecta"));

            return await GenerarTokens(usuarioTemp);
        }

        public async Task<Respuesta<TokenJwt, Mensaje>> RefrescarToken(TokenJwt request)
        {
            Respuesta<TokenJwt, Mensaje> respuesta = new();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(request.Token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

            var idUsuario = Convert.ToInt32(userIdClaim.Value);

            await _persistencia.ValidarTokenRefresco(idUsuario, request.RefreshToken);
            
            var respuestaBusqueda = await _persistencia.BuscarUsuarioPorLogin(string.Empty, idUsuario);

            if (!respuestaBusqueda.EsExitoso)
                return respuesta.RespuestaError(401, respuestaBusqueda.Mensaje);

            return await GenerarTokens(respuestaBusqueda.Objeto);
        }

        private async Task<Respuesta<TokenJwt, Mensaje>> GenerarTokens(Usuario usuario)
        {
            Respuesta<TokenJwt, Mensaje> respuesta = new();
        
            string tokenStr = _generadorTokenJwt.GenerarTokenAcceso(usuario);
            string tokenRefresh = _generadorTokenJwt.GenerarTokenRefresco();

            await _persistencia.GuardarTokenRefresco(usuario.IdUsuario, tokenRefresh);

            TokenJwt token = new()
            {
                Token = tokenStr,
                RefreshToken = tokenRefresh
            };

            return respuesta.RespuestaExito(token);
        }
    }
}
