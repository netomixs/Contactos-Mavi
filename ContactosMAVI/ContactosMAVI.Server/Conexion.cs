using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
namespace ContactosMAVI.Server
{ /**
         * Clase de conexion 
         */
    public class Conexion

    {
        private string DataBaseName = "ServicioAndroid";
        private string User = "UsrExamen";
        private string Password = "U12345";
        private string Server = "Proserver";
        private string cadena = "Server=localhost\\SQLEXPRESS;Database=Contactos;Trusted_Connection=True;TrustServerCertificate=True;";
        private SqlConnection sqlConnection;
        public Conexion()
        {
            //string cadena = $"Server={Server};Database={DataBaseName};User={User};Password={Password};";

            sqlConnection = new SqlConnection()
            {
                ConnectionString = cadena
            };

        }
        /**
         * Obtener el objeto de conexion
         * */
        public SqlConnection GetConexion()
        {
            return sqlConnection;
        }
        /**
         *Abrir la conexio a la base de datos 
         **/
        public void abrir()
        {
            sqlConnection.Open();
        }
        /**
         * Cerrar la conexion con la base de datos
         * */

        public void cerrar()
        {
            sqlConnection.Close();
        }
    }
}
