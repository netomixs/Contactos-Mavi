using ContactosMAVI.Server.Interfaces;
using ContactosMAVI.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactosMAVI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Transacciones : ControllerBase
    {

        public Transacciones()
        {

        }
        /***
    * Obtiene los contactos de las base de datos y los regresa en una lista de tipo Contacto
    */
        [HttpGet]
        [Route("Contacto/{id}")]
        public Contacto? GetContacto(int id)
        {
            const string query = "Sp_ContactosMAVIErnesto_SelectById";
            var conexion = new Conexion();
            try
            {
                conexion.abrir();

                var command = new SqlCommand(query, conexion.GetConexion());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var contecto = new Contacto()
                    {
                        Id = reader.IsDBNull(0) ? default : reader.GetInt32(0),
                        Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Tipo_Contacto = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Telefono_Fijo = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Telefono_Movil = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Nacimiento = reader.GetDateTime(5),
                        Sexo = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Estado_Civil = reader.IsDBNull(7) ? null : reader.GetString(7),
                    };
                    return contecto;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.cerrar();
            }

        }
        /***
        * Obtiene los contactos de las base de datos y los regresa en una lista de tipo Contacto
        */
        [HttpGet]
        public IEnumerable<Contacto> GetContacto()
        {
            const string query = "Sp_ContactosMAVIErnesto_SelectAll";
            var conexion = new Conexion();
            try
            {
                conexion.abrir();
                var contectos = new List<Contacto>();
                var command = new SqlCommand(query, conexion.GetConexion());
                command.CommandType = CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var contecto = new Contacto()
                    {
                        Id = reader.IsDBNull(0) ? default : reader.GetInt32(0),
                        Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Tipo_Contacto = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Telefono_Fijo = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Telefono_Movil = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Nacimiento = reader.GetDateTime(5),
                        Sexo = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Estado_Civil = reader.IsDBNull(7) ? null : reader.GetString(7),
                    };
                    contectos.Add(contecto);
                }
                return contectos;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.cerrar();
            }

        }
        /***
            Recupera todos los contactos de la base de datos y usando QLINK los filtra segun el nombre
            PSD: Lo hice de esta manera para usar QLINK
        */
        [HttpGet]
        [Route("{Nombre}")]
        public IEnumerable<Contacto> GetContacto(string Nombre)
        {
            const string query = "Sp_ContactosMAVIErnesto_SelectAll";
            var conexion = new Conexion();
            try
            {
                conexion.abrir();
                var contectos = new List<Contacto>();
                var command = new SqlCommand(query, conexion.GetConexion());
                command.CommandType = CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var contecto = new Contacto()
                    {
                        Id = reader.IsDBNull(0) ? default : reader.GetInt32(0),
                        Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Tipo_Contacto = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Telefono_Fijo = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Telefono_Movil = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Nacimiento = reader.GetDateTime(5),
                        Sexo = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Estado_Civil = reader.IsDBNull(7) ? null : reader.GetString(7),
                    };
                    contectos.Add(contecto);
                }
                var contactosRespuesta = contectos.Where(contacto => contacto.Nombre.Contains(Nombre, StringComparison.OrdinalIgnoreCase)).ToList();
                return contactosRespuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.cerrar();
            }

        }
        /**
        Elimina un contacto por el ID
        */
        [HttpDelete]
        [Route("{id}")]
        public bool Eliminar(int id)
        {
            const string query = "Sp_ContactosMAVIErnesto_Delete";
            var conexion = new Conexion();

            try
            {
                conexion.abrir();
                var command = new SqlCommand(query, conexion.GetConexion());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.cerrar();
            }
        }
        /**
        Realiza la insercion de un contacto
        */
        [HttpPost]
        public int Insertar(Contacto contacto)
        {
            const string query = "Sp_ContactosMAVIErnesto_Insert";
            var conexion = new Conexion();
            int insertedId = 0;
            try
            {
                conexion.abrir();
                var command = new SqlCommand(query, conexion.GetConexion());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                command.Parameters.AddWithValue("@Tipo_Contacto", contacto.Tipo_Contacto);
                command.Parameters.AddWithValue("@Telefono_Fijo", contacto.Telefono_Fijo);
                command.Parameters.AddWithValue("@Telefono_Movil", contacto.Telefono_Movil);
                command.Parameters.AddWithValue("@Nacimiento", contacto.Nacimiento);
                command.Parameters.AddWithValue("@Sexo", contacto.Sexo);
                command.Parameters.AddWithValue("@Estado_Civil", contacto.Estado_Civil);
                var paramInsertedID = new SqlParameter("@InsertedID", SqlDbType.Int);
                paramInsertedID.Direction = ParameterDirection.Output;
                command.Parameters.Add(paramInsertedID);

                command.ExecuteNonQuery();
                insertedId = (int)paramInsertedID.Value;
                return insertedId;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.cerrar();
            }
        }
        /***
        Realiza la actualizacion del contacto 
        */
        [HttpPut]
        public bool Actualizar(Contacto contacto)
        {
            const string query = "Sp_ContactosMAVIErnesto_Update";
            var conexion = new Conexion();

            try
            {
                conexion.abrir();
                var command = new SqlCommand(query, conexion.GetConexion());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", contacto.Id);
                command.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                command.Parameters.AddWithValue("@Tipo_Contacto", contacto.Tipo_Contacto);
                command.Parameters.AddWithValue("@Telefono_Fijo", contacto.Telefono_Fijo);
                command.Parameters.AddWithValue("@Telefono_Movil", contacto.Telefono_Movil);
                command.Parameters.AddWithValue("@Nacimiento", contacto.Nacimiento);
                command.Parameters.AddWithValue("@Sexo", contacto.Sexo);
                command.Parameters.AddWithValue("@Estado_Civil", contacto.Estado_Civil);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.cerrar();
            }
        }
    }
}
