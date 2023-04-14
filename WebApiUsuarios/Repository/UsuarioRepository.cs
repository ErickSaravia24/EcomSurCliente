using Microsoft.Data.SqlClient;
using System.Data;
using WebApiUsuarios.Models;

namespace WebApiUsuarios.Repository
{
    public class UsuarioRepository:IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("EcomSurConnection");
        }

        public async Task<Usuario> GetUsersById(int id, int idrol)
        {
            Usuario usuario = null;
           

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("spConsultarUsuarioPorIdYRol", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fiIdUsuario", idrol);
                    command.Parameters.AddWithValue("@fiIdUsuario2", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                usuario = new Usuario();
                                usuario.UsuarioId = reader.GetInt32(0);
                                usuario.Nombre = reader.GetString(1);
                                usuario.Amaterno = reader.GetString(2);
                                usuario.Apaterno = reader.GetString(3);
                                usuario.Calle = reader.GetString(4);
                                usuario.Numero = reader.GetString(5);
                                usuario.Colonia = reader.GetString(6);
                                usuario.Correro = reader.GetString(8);
                                usuario.Password = reader.GetString(9);
                                usuario.IdRol = reader.GetInt32(10);
                            }
                        }
                    }
                }
            }

            return usuario;
        }


        public async Task<List<Usuario>> GetUsers(int id)
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                    throw;
                }

                using (SqlCommand command = new SqlCommand("spObtenerUsuariosPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fiUsuarioId", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        try
                        {
                            while (await reader.ReadAsync())
                            {
                                Usuario usuario = new Usuario();
                                usuario.UsuarioId = reader.GetInt32(reader.GetOrdinal("fiUsuarioId"));
                                usuario.Nombre = reader.GetString(reader.GetOrdinal("fcNombre"));
                                usuario.Amaterno = reader.GetString(reader.GetOrdinal("fcAmaterno"));
                                usuario.Apaterno = reader.GetString(reader.GetOrdinal("fcApaterno"));
                                usuario.Calle = reader.GetString(reader.GetOrdinal("fcCalle"));
                                usuario.Numero = reader.GetString(reader.GetOrdinal("fcNumero"));
                                usuario.Colonia = reader.GetString(reader.GetOrdinal("fcColonia"));
                                usuario.Correro = reader.GetString(reader.GetOrdinal("fcCorrero"));
                                usuario.Password = reader.GetString(reader.GetOrdinal("fcPassword"));
                                usuario.IdRol = reader.GetInt32(reader.GetOrdinal("fiIdRol"));

                                usuarios.Add(usuario);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al leer los resultados: {ex.Message}");
                            throw;
                        }
                    }
                }
            }

            return usuarios;
        }


        public async Task<bool> InsertUsers(Usuario Usuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("spInsertarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@fcNombre", Usuario.Nombre);
                        command.Parameters.AddWithValue("@fcAmaterno", Usuario.Amaterno);
                        command.Parameters.AddWithValue("@fcApaterno", Usuario.Apaterno);
                        command.Parameters.AddWithValue("@fcCalle", Usuario.Calle);
                        command.Parameters.AddWithValue("@fcNumero", Usuario.Numero);
                        command.Parameters.AddWithValue("@fcColonia", Usuario.Colonia);
                        command.Parameters.AddWithValue("@fcCorreo", Usuario.Correro);
                        command.Parameters.AddWithValue("@fcPassword", Usuario.Password);
                        command.Parameters.AddWithValue("@fiIdRol", Usuario.IdRol);
                        command.Parameters.AddWithValue("@fiIdUsuario", Usuario.UsuarioId);
                        await command.ExecuteNonQueryAsync();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar el usuario  {Usuario.Nombre}: {ex.Message}");
                return false;
            }
        }

      

        public async Task<bool> UpdateUsers(Usuario Usuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("UpdateUser", connection))
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar el usuario con Id {Usuario.UsuarioId}: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteUsers(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userId", id);
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el usuario con Id {id}: {ex.Message}");
                return false;
            }
        }
    }
}
