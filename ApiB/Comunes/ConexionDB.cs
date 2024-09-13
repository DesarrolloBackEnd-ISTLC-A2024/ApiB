using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ApiB.Comunes
{
    public class ConexionDB
    {
        private static SqlConnection conexion;

        public static SqlConnection abrirConexion()
        {
            conexion = new SqlConnection("Server=PC4_LAB1\\SQLEXPRESS;Database=BaseB;Trusted_Connection=True;TrustServerCertificate=True;");
            conexion.Open();
            return conexion;
        }



    }
}