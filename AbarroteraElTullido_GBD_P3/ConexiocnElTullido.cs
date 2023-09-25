using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient;     -No es necesario utilizarla
using MySqlConnector;

namespace AbarroteraElTullido_GBD_P3
{
    class ConexiocnElTullido
    {
        public static MySqlConnection conexion() 
        {
            string servidor = "localhost";//Prueba --- Local instance MySQL80
            string bd = "abarrote";
            string usuario = "root";
            string contrasena = "root";
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + contrasena + "";
            try
            {
                MySqlConnection conexionDB = new MySqlConnection(cadenaConexion);
                return conexionDB;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
