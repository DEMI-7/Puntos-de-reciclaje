using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MaterialNegocio
    {

        public List<Material> listar()
        {
            List<Material> lista = new List<Material>();

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta("SELECT ID_Material, Nombre, Info FROM Materiales");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    Material aux = new Material();

                    aux.IdMaterial = (long)conexion.Lector["ID_Material"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Info = (string)conexion.Lector["Info"];

                    lista.Add(aux);
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

        public List<Material> ListarConImagenes()
        {
            List<Material> lista = new List<Material>();

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta("SELECT ID_Material, Nombre, Info FROM Materiales");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    Material aux = new Material();

                    aux.IdMaterial = (long)conexion.Lector["ID_Material"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Info = (string)conexion.Lector["Info"];

                    lista.Add(aux);
                }

                // Ahora traemos las imagenes y las asociamos a los materiales
                ImagenMaterialNegocio negocioImagen = new ImagenMaterialNegocio();
                List<ImagenMaterial> imagenes = negocioImagen.Listar();
                foreach (Material m in lista)
                {
                    m.ListaUrlImagenes = imagenes.Where(img => img.ID_Material == m.IdMaterial)
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
    }
}
