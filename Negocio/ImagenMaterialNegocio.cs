using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenMaterialNegocio
    {
        public List<ImagenMaterial> Listar()
        {
            List<ImagenMaterial> lista = new List<ImagenMaterial>();

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta("SELECT ID_Imagen, ID_Material, URL_Imagen FROM Imagen_Material");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {

                    ImagenMaterial imagen = new ImagenMaterial();

                    imagen.ID_Imagen = (long)conexion.Lector["ID_Imagen"];
                    imagen.ID_Material = (long)conexion.Lector["ID_Material"];
                    imagen.URL_Imagen = (string)conexion.Lector["URL_Imagen"];

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
