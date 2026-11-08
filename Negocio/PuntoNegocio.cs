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

                // Ahora traemos las imagenes y las asociamos a los puntos
                ImagenPuntoNegocio negocioImagen = new ImagenPuntoNegocio();
                List<ImagenPuntoReciclaje> imagenes = negocioImagen.Listar(lista);
                foreach (PuntoReciclaje p in lista)
                {
                    p.ListaUrlImagenes = imagenes.Where(img => img.IdPunto == p.IdPunto)
                        .Select(img => img)
                        .ToList();
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
                    xx.IdPunto = (long)datos.Lector["ID_Punto"];
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

                
                System.Data.SqlClient.SqlParameter paramLat = new System.Data.SqlClient.SqlParameter("@latitud", System.Data.SqlDbType.Decimal);
                paramLat.Precision = 12;
                paramLat.Scale = 8;
                paramLat.Value = puntonew.Latitud;

                System.Data.SqlClient.SqlParameter paramLng = new System.Data.SqlClient.SqlParameter("@longitud", System.Data.SqlDbType.Decimal);
                paramLng.Precision = 12;
                paramLng.Scale = 8;
                paramLng.Value = puntonew.Longitud;

                
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


        public void Eliminar (long idPunto)
        {
            Accesodatos datos = new Accesodatos();

            try
            {
                //primero borramos las relaciones digamos (imagenes materiales ; )  q tenga el punto ;

                datos.SetearConsulta("DELETE FROM Material_PuntoReciclaje WHERE ID_Punto = @idPunto");
                datos.agregarParametro("@idPunto", idPunto);

                datos.ejecutarAccion();

                // borramos las imagenes del punto ;
                datos.limpiarParametros();
                datos.SetearConsulta("DELETE FROM Imagen_PuntoReciclaje WHERE ID_PuntoReciclaje = @idPunto");
                datos.agregarParametro("@idPunto", idPunto);
                datos.ejecutarAccion();

                //borramos el punto  ; 
                // antes limpiamos los parametros de la consulta anterior; 

                datos.limpiarParametros();
                datos.SetearConsulta("DELETE FROM Puntos_reciclaje WHERE ID_Punto = @idPunto");

                datos.agregarParametro("@idPunto", idPunto);
                datos.ejecutarAccion(); 

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modificar(PuntoReciclaje punto)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta(@"UPDATE Puntos_reciclaje 
                               SET Direccion = @direccion, Latitud = @latitud, Longitud = @longitud, Horario = @horario 
                               WHERE ID_Punto = @idPunto");

                datos.agregarParametro("@direccion", punto.Direccion);

                // Usamos Math.Round para evitar el error de overflow en el UPDATE

                datos.agregarParametro("@latitud", Math.Round(punto.Latitud, 8));

                datos.agregarParametro("@longitud", Math.Round(punto.Longitud, 8));

                datos.agregarParametro("@horario", punto.Horario);

                datos.agregarParametro("@idPunto", punto.IdPunto);

                datos.ejecutarAccion();
            }

            catch (Exception)
            {
                throw;
            }
        }
        public void EliminarMaterialesDelPunto(long idPunto)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta("DELETE FROM Material_PuntoReciclaje WHERE ID_Punto = @idPunto");
                datos.agregarParametro("@idPunto", idPunto);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}