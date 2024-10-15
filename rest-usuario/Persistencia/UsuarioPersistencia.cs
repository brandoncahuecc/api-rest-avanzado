using Dapper;
using MySql.Data.MySqlClient;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace rest_usuario.Persistencia
{
    public interface IUsuarioPersistencia
    {
        Task<Respuesta<Usuario, Mensaje>> BuscarUsuarioPorLogin(string login, int usuarioId);
        Task<Respuesta<Mensaje, Mensaje>> GuardarTokenRefresco(int usuarioId, string tokenRefresco);
        Task<Respuesta<Mensaje, Mensaje>> ValidarTokenRefresco(int usuarioId, string tokenRefresco);
    }

    public class UsuarioPersistencia : IUsuarioPersistencia
    {
        private readonly string _cadenaConexion;

        public UsuarioPersistencia()
        {
            _cadenaConexion = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
        }

        public async Task<Respuesta<Usuario, Mensaje>> BuscarUsuarioPorLogin(string login, int usuarioId)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                Respuesta<Usuario, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    string sql = @"SELECT 
idusuario,
nombre,
tipo_documento as tipodocumento,
num_documento as numdocumento,
direccion,
telefono,
email,
cargo,
login,
clave,
imagen,
idsucursal,
condicion
FROM usuario WHERE condicion = 1 AND (login = @Login OR idusuario = @IdUsuario);";

                    await conn.OpenAsync();

                    var resultado = await conn.QueryFirstAsync<Usuario>(sql, new { Login = login, IdUsuario = usuarioId });

                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado);

                    mensaje = new("NO-EXIST-DB", "Usuario no existe en la base de datos");
                    return respuesta.RespuestaError(400, mensaje);
                }
                catch (Exception ex)
                {
                    mensaje = new("NO-CON-DB", "Tenemos problemas con la base de datos, reporte al administrador", ex);
                    return respuesta.RespuestaError(500, mensaje);
                }
                finally
                {
                    if (conn is not null)
                        await conn.CloseAsync();
                }
            }
        }

        public async Task<Respuesta<Mensaje, Mensaje>> GuardarTokenRefresco(int usuarioId, string tokenRefresco)
        {
            Respuesta<Mensaje, Mensaje> respuesta = new();

            /*
             * Todo el código necesario para almacenar el token en la base de datos
             */

            return respuesta.RespuestaExito(new Mensaje("SUCCESS", "Token almacenado"));
        }

        public async Task<Respuesta<Mensaje, Mensaje>> ValidarTokenRefresco(int usuarioId, string tokenRefresco)
        {
            Respuesta<Mensaje, Mensaje> respuesta = new();

            /*
             * Todo el código necesario para validar el token en la base de datos
             */

            return respuesta.RespuestaExito(new Mensaje("SUCCESS", "Token almacenado"));
        }
    }
}
