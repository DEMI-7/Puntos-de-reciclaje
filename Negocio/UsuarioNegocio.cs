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

        public bool AprobarEmpresa(RegistroAccion registro)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET EstadoCuenta = 'aprobado' WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", registro.UsuarioAfectado.ID_Usuario);
                conexion.ejecutarAccion();


                RegistroAccionNegocio negocio = new RegistroAccionNegocio();
                negocio.RegistrarMovimiento(registro);


                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RechazarEmpresa(RegistroAccion registro)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET EstadoCuenta = 'rechazado' WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", registro.UsuarioAfectado.ID_Usuario);
                conexion.ejecutarAccion();

                RegistroAccionNegocio negocio = new RegistroAccionNegocio();
                negocio.RegistrarMovimiento(registro);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void MandarAPendiente(RegistroAccion registro)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET EstadoCuenta = 'pendiente', Activo = 1 WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", registro.UsuarioAfectado.ID_Usuario);
                conexion.ejecutarAccion();

                RegistroAccionNegocio negocio = new RegistroAccionNegocio();
                negocio.RegistrarMovimiento(registro);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DesactivarUsuario(RegistroAccion registro)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET Activo = 0 WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", registro.UsuarioAfectado.ID_Usuario);
                conexion.ejecutarAccion();

                RegistroAccionNegocio negocio = new RegistroAccionNegocio();
                negocio.RegistrarMovimiento(registro);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ActivarUsuario(RegistroAccion registro)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET Activo = 1 WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@idUsuario", registro.UsuarioAfectado.ID_Usuario);
                conexion.ejecutarAccion();

                RegistroAccionNegocio negocio = new RegistroAccionNegocio();
                negocio.RegistrarMovimiento(registro);

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

        public bool Loguear(Usuario usuario)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                
                conexion.SetearConsulta("SELECT ID_Usuario, Nombre, TipoUsuario, EstadoCuenta, Activo, Contraseña_hash FROM Usuarios WHERE Email = @email");
                conexion.agregarParametro("@email", usuario.Email);
                conexion.EjecutarLectura();

                
                if (conexion.Lector.Read())
                {
                    // comparamos la contraseña escrita con la guardada en la base, ejecutamos en c#,para distinguir mayus
                    string contraseñaBase = (string)conexion.Lector["Contraseña_hash"];
                    if (contraseñaBase != usuario.Contraseña)
                    {
                        return false;
                    }
                    // si la clave coincide, completamos con el usuario ingresado
                    usuario.ID_Usuario = (long)conexion.Lector["ID_Usuario"];
                    usuario.Nombre = (string)conexion.Lector["Nombre"];
                    usuario.TipoUsuario = (string)conexion.Lector["TipoUsuario"];
                    usuario.EstadoCuenta = (string)conexion.Lector["EstadoCuenta"];
                    usuario.Activo = (bool)conexion.Lector["Activo"];
                    return true;
                }

                return false;
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

        public void CambiarClave(long idUsuario, string claveNueva)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("UPDATE Usuarios SET Contraseña_hash = @claveNueva WHERE ID_Usuario = @idUsuario");
                conexion.agregarParametro("@claveNueva", claveNueva);
                conexion.agregarParametro("@idUsuario", idUsuario);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public long VerificarPalabras(string email, string palabra1, string palabra2)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("SELECT ID_Usuario, PalabraRecuperacion1, PalabraRecuperacion2 FROM Usuarios WHERE Email = @email");
                conexion.agregarParametro("@email", email);
                conexion.EjecutarLectura();

                if (conexion.Lector.Read())
                {
                    // si el usuario nunca cargo palabras no se puede recuperar por aca
                    if (conexion.Lector["PalabraRecuperacion1"] == DBNull.Value || conexion.Lector["PalabraRecuperacion2"] == DBNull.Value)
                    {
                        return 0;
                    }

                    string palabra1Base = (string)conexion.Lector["PalabraRecuperacion1"];
                    string palabra2Base = (string)conexion.Lector["PalabraRecuperacion2"];

                    // si coinciden las dos, devolvemos el ID del usuario
                    if (palabra1Base == palabra1 && palabra2Base == palabra2)
                    {
                        return (long)conexion.Lector["ID_Usuario"];
                    }
                }

                // no existe el email o no coinciden las palabras
                return 0;
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
                conexion.SetearConsulta("INSERT INTO Usuarios (Nombre, Email, Contraseña_hash, TipoUsuario, EstadoCuenta) VALUES (@nombre, @email, @contraseña, @tipoUsuario, @estadoCuenta, @palabra1, @palabra2)");
                conexion.agregarParametro("@nombre", nuevo.Nombre);
                conexion.agregarParametro("@email", nuevo.Email);
                conexion.agregarParametro("@contraseña", nuevo.Contraseña);
                conexion.agregarParametro("@tipoUsuario", nuevo.TipoUsuario);
                conexion.agregarParametro("@estadoCuenta", nuevo.EstadoCuenta);
                conexion.agregarParametro("@palabra1", nuevo.PalabraRecuperacion1);
                conexion.agregarParametro("@palabra2", nuevo.PalabraRecuperacion2);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
