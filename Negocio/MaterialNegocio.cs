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
                conexion.SetearConsulta("SELECT ID_Material, Nombre, Info FROM Materiales Where Activo = 1");
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
                    if (conexion.Lector["UrlLogo"] != DBNull.Value)
                    {
                        aux.UrlLogo = (string)conexion.Lector["UrlLogo"];
                    }
                    else
                    {
                        aux.UrlLogo = "";
                    }

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

        public void Agregar(Material nuevo)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("Insert Into Materiales (Nombre, Info, UrlLogo) Values (@nombre, @info, @urlLogo)");
                conexion.agregarParametro("@nombre", nuevo.Nombre);
                conexion.agregarParametro("@info", nuevo.Info);
                conexion.agregarParametro("@urlLogo", nuevo.UrlLogo);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modificar(Material material)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("Update Materiales Set Nombre = @nombre, Info = @info, UrlLogo = @urlLogo WHERE ID_Material = @idMaterial");
                conexion.agregarParametro("@nombre", material.Nombre);
                conexion.agregarParametro("@info", material.Info);
                conexion.agregarParametro("@idMaterial", material.IdMaterial);
                conexion.agregarParametro("@urlLogo", material.UrlLogo);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Eliminar(long idMaterial)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("Update Materiales Set Activo = 0 Where ID_Material = @idMaterial");
                conexion.agregarParametro("@idMaterial", idMaterial);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
