using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class ImagenPuntoNegocio
    {

        public List<ImagenPuntoReciclaje> Listar(List<PuntoReciclaje> puntos)
        {
            List<ImagenPuntoReciclaje> lista = new List<ImagenPuntoReciclaje>();

            string idsPuntos = string.Join(",", puntos.Select(p => p.IdPunto));

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta($"SELECT ID_Imagen, ID_PuntoReciclaje, URL_Imagen FROM Imagen_PuntoReciclaje WHERE ID_PuntoReciclaje IN ({idsPuntos})");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {

                    ImagenPuntoReciclaje imagen = new ImagenPuntoReciclaje();

                    imagen.IdImagen = (long)conexion.Lector["ID_Imagen"];
                    imagen.IdPunto = (long)conexion.Lector["ID_PuntoReciclaje"];
                    imagen.UrlImagen = (string)conexion.Lector["URL_Imagen"];
                    
                    lista.Add(imagen);
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

        public void Agregar(ImagenPuntoReciclaje imagen)
        {
            Accesodatos datobich = new Accesodatos();
            try
            {
                datobich.SetearConsulta("INSERT INTO Imagen_PuntoReciclaje (ID_PuntoReciclaje, URL_Imagen) VALUES (@idPunto, @url)");
                datobich.agregarParametro("@idPunto", imagen.IdPunto);
                datobich.agregarParametro("@url", imagen.UrlImagen);
                datobich.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;

            }
        }

        public void Eliminar(long idImagen)
        {
            Accesodatos datos = new Accesodatos();

            try
            {
                datos.SetearConsulta("DELETE FROM Imagen_PuntoReciclaje WHERE ID_Imagen = @idImagen");

                datos.agregarParametro("@idImagen", idImagen);
                datos.ejecutarAccion();


            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
