using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> ListarActivos()
        {
            List<Usuario> lista = new List<Usuario>();

            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("SELECT ID_Usuario, Nombre, Email, TipoUsuario, EstadoCuenta FROM Usuarios WHERE Activo = 1 AND EstadoCuenta ='aprobado'");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.ID_Usuario = (long)conexion.Lector["ID_Usuario"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Email = (string)conexion.Lector["Email"];
                    aux.TipoUsuario = (string)conexion.Lector["TipoUsuario"];
                    aux.EstadoCuenta = (string)conexion.Lector["EstadoCuenta"];

                    lista.Add(aux);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }



            return lista;
        }

        public List<Usuario> ListarEmpresasPendientes()
        {
            List<Usuario> lista = new List<Usuario>();

            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("SELECT ID_Usuario, Nombre, Email, TipoUsuario, EstadoCuenta FROM Usuarios WHERE Activo = 1 AND (TipoUsuario ='empresa' OR TipoUsuario='gubernamental') AND EstadoCuenta ='pendiente'");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.ID_Usuario = (long)conexion.Lector["ID_Usuario"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Email = (string)conexion.Lector["Email"];
                    aux.TipoUsuario = (string)conexion.Lector["TipoUsuario"];
                    aux.EstadoCuenta = (string)conexion.Lector["EstadoCuenta"];

                    lista.Add(aux);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }



            return lista;
        }

        public List<Usuario> ListarDesactivados()
        {
            List<Usuario> lista = new List<Usuario>();

            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("SELECT ID_Usuario, Nombre, Email, TipoUsuario, EstadoCuenta FROM Usuarios WHERE Activo = 0 AND EstadoCuenta ='aprobado'");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.ID_Usuario = (long)conexion.Lector["ID_Usuario"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Email = (string)conexion.Lector["Email"];
                    aux.TipoUsuario = (string)conexion.Lector["TipoUsuario"];
                    aux.EstadoCuenta = (string)conexion.Lector["EstadoCuenta"];

                    lista.Add(aux);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }



            return lista;
        }

        public List<Usuario> ListarEmpresasRechazadas()
        {
            List<Usuario> lista = new List<Usuario>();

            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("SELECT ID_Usuario, Nombre, Email, TipoUsuario, EstadoCuenta FROM Usuarios WHERE Activo = 1 AND (TipoUsuario ='empresa' OR TipoUsuario='gubernamental') AND EstadoCuenta ='rechazado'");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.ID_Usuario = (long)conexion.Lector["ID_Usuario"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Email = (string)conexion.Lector["Email"];
                    aux.TipoUsuario = (string)conexion.Lector["TipoUsuario"];
                    aux.EstadoCuenta = (string)conexion.Lector["EstadoCuenta"];

                    lista.Add(aux);


                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return lista;
        }

        public void AprobarEmpresa(long idUsuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET EstadoCuenta = 'aprobado' WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", idUsuario);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
