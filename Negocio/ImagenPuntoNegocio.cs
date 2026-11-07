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
    }
}
