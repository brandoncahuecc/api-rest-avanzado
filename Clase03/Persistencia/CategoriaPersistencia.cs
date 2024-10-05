using Clase03.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using static Mysqlx.Expect.Open.Types;

namespace Clase03.Persistencia
{
    public interface ICategoriaPersistencia
    {
        Task<List<Categoria>> ObtenerTodos();
        Task<Categoria> ObtenerUno(int id);
        Task<Categoria> Crear(Categoria request);
        Task<Categoria> Actualizar(Categoria request);
        Task<bool> Eliminar(int id);
    }

    public class CategoriaPersistencia : ICategoriaPersistencia
    {
        private readonly string _cadenaConexion;

        public CategoriaPersistencia()
        {
            _cadenaConexion = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
        }

        public async Task<Categoria> Actualizar(Categoria request)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
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
                        return request;

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn is not null)
                        await conn.CloseAsync();
                }
            }
        }

        public async Task<Categoria> Crear(Categoria request)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
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
                        return request;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn is not null)
                        await conn.CloseAsync();
                }
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
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
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (conn is not null)
                        await conn.CloseAsync();
                }
            }
        }

        public async Task<List<Categoria>> ObtenerTodos()
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                try
                {
                    string sql = "SELECT idcategoria, nombre, descripcion, condicion FROM categoria WHERE condicion = @Condicion;";

                    await conn.OpenAsync();

                    var resultado = await conn.QueryAsync<Categoria>(sql, new { Condicion = 1 });

                    return resultado.ToList();
                }
                catch (Exception ex)
                {
                    return new List<Categoria>();
                }
                finally
                {
                    if (conn is not null)
                        await conn.CloseAsync();
                }
            }
        }

        public async Task<Categoria> ObtenerUno(int id)
        {
            using (MySqlConnection conn = new(_cadenaConexion))
            {
                try
                {
                    string sql = @"SELECT idcategoria, nombre, descripcion, condicion 
FROM categoria WHERE condicion = @Condicion AND idcategoria = @Id;";

                    await conn.OpenAsync();

                    var resultado = await conn.QueryFirstAsync<Categoria>(sql, new { Condicion = 1, Id = id });

                    return resultado;
                }
                catch (Exception ex)
                {
                    return null;
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
