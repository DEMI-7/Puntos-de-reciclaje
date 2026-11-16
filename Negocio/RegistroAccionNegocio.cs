using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RegistroAccionNegocio
    {

        public List<RegistroAccion> Listar()
        {
            List<RegistroAccion> lista = new List<RegistroAccion>();

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta("SELECT ID_Movimiento, EstadoAnterior, EstadoNuevo,Detalle, HoraMovimiento, ID_Admin, A.Nombre AS NombreAdmin, A.Email AS EmailAdmin, ID_UsuarioAfectado, U.Nombre AS NombreUsuario, U.Email AS EmailUsuario, U.TipoUsuario as TipoUsuario FROM RegistroAcciones Reg INNER JOIN Usuarios A ON Reg.ID_Admin = A.ID_Usuario INNER JOIN Usuarios U ON Reg.ID_UsuarioAfectado = U.ID_Usuario;");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    RegistroAccion aux = new RegistroAccion();
                    Usuario auxAdmin = new Usuario();
                    Usuario auxUsuario = new Usuario();

                    aux.ID_Movimiento = (long)conexion.Lector["ID_Movimiento"];
                    aux.EstadoAnterior = (string)conexion.Lector["EstadoAnterior"];
                    aux.EstadoNuevo = (string)conexion.Lector["EstadoNuevo"];
                    aux.Detalle = (string)conexion.Lector["Detalle"];
                    aux.HoraMovimiento = (DateTime)conexion.Lector["HoraMovimiento"];

                    auxAdmin.ID_Usuario = (long)conexion.Lector["ID_Admin"];
                    auxAdmin.Nombre = (string)conexion.Lector["NombreAdmin"];
                    auxAdmin.Email = (string)conexion.Lector["EmailAdmin"];
                    auxUsuario.ID_Usuario = (long)conexion.Lector["ID_UsuarioAfectado"];
                    auxUsuario.Nombre = (string)conexion.Lector["NombreUsuario"];
                    auxUsuario.Email = (string)conexion.Lector["EmailUsuario"];
                    auxUsuario.TipoUsuario = (string)conexion.Lector["TipoUsuario"];

                    aux.Admin = auxAdmin;
                    aux.UsuarioAfectado = auxUsuario;

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
        
        public void RegistrarMovimiento (RegistroAccion nuevo)
        {
            Accesodatos conexion = new Accesodatos();
            try
            {
                conexion.SetearConsulta("Insert Into RegistroAcciones (ID_Admin, ID_UsuarioAfectado, EstadoAnterior, EstadoNuevo, Detalle) Values (@ID_Admin, @ID_UsuarioAfectado, @EstadoAnterior, @EstadoNuevo, @Detalle)");
                conexion.agregarParametro("@ID_Admin", nuevo.Admin.ID_Usuario);
                conexion.agregarParametro("@ID_UsuarioAfectado", nuevo.UsuarioAfectado.ID_Usuario);
                conexion.agregarParametro("@EstadoAnterior", nuevo.EstadoAnterior);
                conexion.agregarParametro("@EstadoNuevo", nuevo.EstadoNuevo);
                conexion.agregarParametro("@Detalle", nuevo.Detalle);
                conexion.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
