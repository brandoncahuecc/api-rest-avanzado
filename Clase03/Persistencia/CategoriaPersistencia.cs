using Dapper;
using MySql.Data.MySqlClient;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace Clase03.Persistencia
{
    public interface ICategoriaPersistencia
    {
        Task<Respuesta<List<Categoria>, Mensaje>> ObtenerTodos();
        Task<Respuesta<Categoria, Mensaje>> ObtenerUno(int id);
        Task<Respuesta<Categoria, Mensaje>> Crear(Categoria request);
        Task<Respuesta<Categoria, Mensaje>> Actualizar(Categoria request);
        Task<Respuesta<Mensaje, Mensaje>> Eliminar(int id);
    }

    public class CategoriaPersistencia : ICategoriaPersistencia
    {
        private readonly string _cadenaConexion;

        public CategoriaPersistencia()
        {
            _cadenaConexion = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
        }

        public async Task<Respuesta<Categoria, Mensaje>> Actualizar(Categoria request)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                Respuesta<Categoria, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    string sql = @"UPDATE categoria
SET nombre = @Nombre,
descripcion = @Descripcion
WHERE idcategoria=@Id;";

                    DynamicParameters parametros = new();
                    parametros.Add("Nombre", request.Nombre);
                    parametros.Add("Descripcion", request.Descripcion);
                    parametros.Add("Id", request.IdCategoria);

                    await conn.OpenAsync();

                    var resultado = await conn.ExecuteAsync(sql, parametros);

                    if (resultado > 0)
                        return respuesta.RespuestaExito(request);

                    mensaje = new("NO-UPDATE-DB", "No fue posible actualizar la categoria, vuelva a intertarlo o valide que exista");
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

        public async Task<Respuesta<Categoria, Mensaje>> Crear(Categoria request)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                Respuesta<Categoria, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    string sql = @"INSERT INTO categoria
(nombre, descripcion, condicion)
VALUES(@Nombre, @Descripcion, @Condicion);";

                    DynamicParameters parametros = new();
                    parametros.Add("Nombre", request.Nombre);
                    parametros.Add("Descripcion", request.Descripcion);
                    parametros.Add("Condicion", request.Condicion);

                    await conn.OpenAsync();

                    var resultado = await conn.ExecuteAsync(sql, parametros);

                    if (resultado > 0)
                    {
                        int lastId = await conn.QueryFirstAsync<int>("SELECT LAST_INSERT_ID()");
                        request.IdCategoria = lastId;
                        return respuesta.RespuestaExito(request);
                    }

                    mensaje = new("NO-CREATE-DB", "No fue posible crear la categoria, revise los datos proporcionados y vuelva a intentarlo");
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

        public async Task<Respuesta<Mensaje, Mensaje>> Eliminar(int id)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                Respuesta<Mensaje, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    string sql = @"UPDATE categoria
SET condicion = @Condicion 
WHERE idcategoria=@Id;";

                    DynamicParameters parametros = new();
                    parametros.Add("Condicion", 0);
                    parametros.Add("Id", id);

                    await conn.OpenAsync();

                    var resultado = await conn.ExecuteAsync(sql, parametros);

                    if (resultado > 0)
                    {
                        mensaje = new("SUCCESS", "Categoría eliminada exitosamente");
                        return respuesta.RespuestaExito(mensaje);
                    }

                    mensaje = new("NO-DELETE-DB", "Categoría no fue posible eliminarla, valide si existe dentro de los registros");
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

        public async Task<Respuesta<List<Categoria>, Mensaje>> ObtenerTodos()
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                Respuesta<List<Categoria>, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    string sql = "SELECT idcategoria, nombre, descripcion, condicion FROM categoria WHERE condicion = @Condicion;";

                    await conn.OpenAsync();

                    var resultado = await conn.QueryAsync<Categoria>(sql, new { Condicion = 1 });

                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado.ToList());

                    mensaje = new("NO-EXIST-DB", "No existen categorias en la base de datos");
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

        public async Task<Respuesta<Categoria, Mensaje>> ObtenerUno(int id)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                Respuesta<Categoria, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    string sql = @"SELECT idcategoria, nombre, descripcion, condicion 
FROM categoria WHERE condicion = @Condicion AND idcategoria = @Id;";

                    await conn.OpenAsync();

                    var resultado = await conn.QueryFirstAsync<Categoria>(sql, new { Condicion = 1, Id = id });
                    
                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado);

                    mensaje = new("NO-EXIST-DB", "Categorias no existe en la base de datos");
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
    }
}
