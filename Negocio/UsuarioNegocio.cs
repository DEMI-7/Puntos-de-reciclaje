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
        public void RechazarEmpresa(long idUsuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET EstadoCuenta = 'rechazado' WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", idUsuario);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void MandarAPendiente(long idUsuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET EstadoCuenta = 'pendiente', Activo = 1 WHERE ID_Usuario = @idUsuario"); conexion.agregarParametro("@idUsuario", idUsuario);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DesactivarUsuario(long idUsuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET Activo = 0 WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", idUsuario);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ActivarUsuario(long idUsuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET Activo = 1 WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", idUsuario);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modificar(Usuario usuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET Nombre = @nombre, Email = @email, TipoUsuario = @tipoUsuario WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@nombre", usuario.Nombre);
                conexion.agregarParametro("@email", usuario.Email);
                conexion.agregarParametro("@tipoUsuario", usuario.TipoUsuario);
                conexion.agregarParametro("@idUsuario", usuario.ID_Usuario);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ExisteEmail(string email, long idUsuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("SELECT COUNT(*) FROM Usuarios WHERE Email = @email AND ID_Usuario <> @idUsuario");
                conexion.agregarParametro("@email", email);
                conexion.agregarParametro("@idUsuario", idUsuario);

                int cantidad = Convert.ToInt32(conexion.ejecutarAccionScalar());
                return cantidad > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarPorEmail(string email)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("SELECT ID_Usuario, Nombre, Email, TipoUsuario, EstadoCuenta FROM Usuarios WHERE Email = @email");
                conexion.agregarParametro("@email", email);
                conexion.EjecutarLectura();

                // si encontro una fila se arma el usuario
                if (conexion.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.ID_Usuario = (long)conexion.Lector["ID_Usuario"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Email = (string)conexion.Lector["Email"];
                    aux.TipoUsuario = (string)conexion.Lector["TipoUsuario"];
                    aux.EstadoCuenta = (string)conexion.Lector["EstadoCuenta"];

                    return aux;
                }

                // si no encontro nada devuelve null
                return null;
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

        public void Agregar(Usuario nuevo)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("INSERT INTO Usuarios (Nombre, Email, Contraseña_hash, TipoUsuario, EstadoCuenta) VALUES (@nombre, @email, @contraseña, @tipoUsuario, @estadoCuenta)");
                conexion.agregarParametro("@nombre", nuevo.Nombre);
                conexion.agregarParametro("@email", nuevo.Email);
                conexion.agregarParametro("@contraseña", nuevo.Contraseña);
                conexion.agregarParametro("@tipoUsuario", nuevo.TipoUsuario);
                conexion.agregarParametro("@estadoCuenta", nuevo.EstadoCuenta);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
