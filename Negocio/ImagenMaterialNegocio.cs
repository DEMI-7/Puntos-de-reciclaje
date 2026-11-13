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
                conexion.SetearConsulta("SELECT ID_Imagen, ID_Material, URL_Imagen FROM Imagen_Material WHERE Activa = 1");
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

        public void SubirImagenPaginaMaterial(ImagenMaterial imagen)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("INSERT INTO Imagen_Material (ID_Material, URL_Imagen) VALUES (@ID_Material, @URL_Imagen)");
                
                conexion.agregarParametro("@ID_Material", imagen.ID_Material);
                conexion.agregarParametro("@URL_Imagen", imagen.URL_Imagen);
                conexion.ejecutarAccion();
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

        public ImagenMaterial BuscarImagenPorId(long ID_Imagen)
        {
            Accesodatos conexion = new Accesodatos();
            ImagenMaterial imagen = new ImagenMaterial();

            try
            {
                conexion.SetearConsulta("SELECT ID_Imagen, ID_Material, URL_Imagen FROM Imagen_Material WHERE ID_Imagen = @ID_Imagen");
                conexion.agregarParametro("@ID_Imagen", ID_Imagen);
                conexion.EjecutarLectura();

                if (conexion.Lector.Read())
                {
                    imagen.ID_Imagen = (long)conexion.Lector["ID_Imagen"];
                    imagen.ID_Material = (long)conexion.Lector["ID_Material"];
                    imagen.URL_Imagen = (string)conexion.Lector["URL_Imagen"];
                }

                return imagen;
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

        public void BorradoLogicoImagenMaterial(long ID_Imagen)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Imagen_Material SET Activa = 0 WHERE ID_Imagen = @ID_Imagen");
                conexion.agregarParametro("@ID_Imagen", ID_Imagen);
                conexion.ejecutarAccion();
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
