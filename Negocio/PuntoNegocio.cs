using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dominio;


namespace Negocio
{
    public class PuntoNegocio
    {
        public List<PuntoReciclaje> listar()
        {
            List<PuntoReciclaje> lista = new List<PuntoReciclaje>();

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta("SELECT p.ID_Punto, p.ID_UsuarioCreador, p.Direccion, p.Latitud, p.Longitud, p.Horario, m.ID_Material, m.Nombre AS NombreMaterial FROM Puntos_reciclaje p LEFT JOIN Material_PuntoReciclaje pm ON p.ID_Punto = pm.ID_Punto LEFT JOIN Materiales m ON pm.ID_Material = m.ID_Material");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {

                    long idPunto = (long)conexion.Lector["ID_Punto"];

                    // Verificamos si el punto ya existe en la lista
                    PuntoReciclaje punto = lista.FirstOrDefault(p => p.IdPunto == idPunto);

                    if (punto == null)
                    {
                        // Si el punto no existe, lo creamos y lo agregamos a la lista
                        punto = new PuntoReciclaje();

                        punto.IdPunto = idPunto;
                        punto.IdUsuarioCreador = (long)conexion.Lector["ID_UsuarioCreador"];
                        punto.Direccion = (string)conexion.Lector["Direccion"];
                        punto.Latitud = (decimal)conexion.Lector["Latitud"];
                        punto.Longitud = (decimal)conexion.Lector["Longitud"];
                        punto.Horario = (string)conexion.Lector["Horario"];

                        lista.Add(punto);
                    }

                    // Ahora, para cada punto, obtenemos los materiales asociados
                    if (conexion.Lector["ID_Material"] != DBNull.Value)
                    {
                        Material mat = new Material();
                        mat.IdMaterial = (long)conexion.Lector["ID_Material"];
                        mat.Nombre = (string)conexion.Lector["NombreMaterial"];
                        punto.ListaMateriales.Add(mat);
                    }
                }


                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }

        }
        public List<PuntoReciclaje> ListarporEmpresa(long idEmpresa)
        {
            List<PuntoReciclaje> lista = new List<PuntoReciclaje>();
            Accesodatos datos = new Accesodatos();
            try
            {
                //traemos los puntos de una emp especifica ; 
                //materiales : simulamos en un string de momento ; 

                datos.SetearConsulta("SELECT ID_Punto, ID_UsuarioCreador, Direccion, Latitud, Longitud," +
                    " Horario FROM Puntos_reciclaje WHERE ID_UsuarioCreador = @idEmpresa");


                datos.agregarParametro("@idEmpresa", idEmpresa);

                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {
                    PuntoReciclaje xx = new PuntoReciclaje();
                    xx.IdPunto = (int)datos.Lector["ID_Punto"];
                    xx.IdUsuarioCreador = (long)datos.Lector["ID_UsuarioCreador"];
                    xx.Direccion = (string)datos.Lector["Direccion"];
                    xx.Latitud = (decimal)datos.Lector["Latitud"];
                    xx.Longitud = (decimal)datos.Lector["Longitud"];
                    xx.Horario = (string)datos.Lector["Horario"];

                    //simulamos  materiales ; 
                    //xx.Materiales = "Ver detalle"; 

                    lista.Add(xx);
                }
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // REVISAR _________________ -----EZE --------------------------------------------
        public long Agregar(PuntoReciclaje puntonew)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta(@"INSERT INTO Puntos_reciclaje (ID_UsuarioCreador, Direccion, Latitud, Longitud, Horario) 
                               OUTPUT INSERTED.ID_Punto 
                               VALUES (@idUsuarioCreador, @direccion, @latitud, @longitud, @horario)");

                datos.agregarParametro("@idUsuarioCreador", puntonew.IdUsuarioCreador);
                datos.agregarParametro("@direccion", puntonew.Direccion);
                datos.agregarParametro("@horario", puntonew.Horario);

                // Usamos SqlParameter para forzar el tipo exacto y que no falle
                System.Data.SqlClient.SqlParameter paramLat = new System.Data.SqlClient.SqlParameter("@latitud", System.Data.SqlDbType.Decimal);
                paramLat.Precision = 12;
                paramLat.Scale = 8;
                paramLat.Value = puntonew.Latitud;

                System.Data.SqlClient.SqlParameter paramLng = new System.Data.SqlClient.SqlParameter("@longitud", System.Data.SqlDbType.Decimal);
                paramLng.Precision = 12;
                paramLng.Scale = 8;
                paramLng.Value = puntonew.Longitud;

                // Si tu clase AccesoDatos no tiene este método, podés usar el AddWithValue normal, 
                // pero asegurándote de que los decimales ya vengan bien redondeados de la página.
                datos.agregarParametro("@latitud", paramLat.Value);
                datos.agregarParametro("@longitud", paramLng.Value);

                object resultado = datos.ejecutarAccionScalar();
                return Convert.ToInt64(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void AgregarMaterialalPunto(long idpuntobich, long idmaterial)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Material_PuntoReciclaje (ID_Punto, ID_Material) VALUES (@idPunto, @idMaterial)");
                datos.agregarParametro("@idPunto", idpuntobich);
                datos.agregarParametro("@idMaterial", idmaterial);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}