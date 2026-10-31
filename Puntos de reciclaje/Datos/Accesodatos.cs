using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Puntos_de_reciclaje.Datos
{
    public class Accesodatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector = null; // Le puse el null! porque sino tiraba un warnin que no podia ser null
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public Accesodatos()
        {
            conexion = new SqlConnection("Server=localhost,1433;Database=CATALOGO_P3_DB;User Id=sa;Password=Grupo0_1234!;TrustServerCertificate=True;");
            comando = new SqlCommand();
        }

        // Recibe la consulta SQL que sera ejecutada
        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        // Lanzara una excepcion si la conexion falla
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Obligatorio cerrar la conexion siempre
        public void CerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open(); //abrimos conexion
                comando.ExecuteNonQuery();
                // se encarga de ejercutar
                // insert, update y delete
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();  //cerramos conexion 
            }

        }

        //agrega un parametro al comando
        public void agregarParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        //limpia los parametros del comando
        public void limpiarParametros()
        {
            comando.Parameters.Clear();
        }



    }
}