using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puntos_de_reciclaje
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void BotonLogin_click(object sender, EventArgs e)
        {
            LblError.Visible = false;

            string email = TxtEmail.Text.Trim();

            // validamos que haya escrito un email
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(TxtContraseña.Text))
            {
                LblError.Text = "Por favor, ingresá tu correo electrónico y contraseña";
                LblError.Visible = true;
                return;
            }
            // buscamos el usuario en la base 
            try
            {
                Usuario usuario = new Usuario();
                usuario.Email = email;
                usuario.Contraseña = TxtContraseña.Text;
                UsuarioNegocio negocio = new UsuarioNegocio();
              
                if (!negocio.Loguear(usuario))
                {
                    LblError.Text = "Mail o contraseña incorrectos";
                    LblError.Visible = true;
                    return;
                }

                // validacion  del estado de la cuenta antes de dejarlo entrar
                if (!usuario.Activo)
                {
                    LblError.Text = "Tu cuenta está desactivada. Consulte con un administrador.";
                    LblError.Visible = true;
                    return;
                }

                if (usuario.EstadoCuenta == "pendiente")
                {
                    LblError.Text = "Tu cuenta aun se encuentra pendiente de aprobación.";
                    LblError.Visible = true;
                    return;
                }

                if (usuario.EstadoCuenta == "rechazado")
                {
                    LblError.Text = "Tu cuenta fue rechazada. Consulte con un administrador.";
                    LblError.Visible = true;
                    return;
                }

                // guardamos en la sesion el usuario que entro
                Session["usuario"] = usuario;

                // lo mandamos a su pagina segun el tipo de usuario
                if (usuario.TipoUsuario == "admin")
                {
                    Response.Redirect("~/PaginasAdmin/PanelAdminCentral.aspx", false);
                }
                else if (usuario.TipoUsuario == "empresa" || usuario.TipoUsuario == "gubernamental")
                {
                    Response.Redirect("PanelEmpresa.aspx", false);
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            catch (Exception ex)
            {
                LblError.Text = "Error al ingresar: " + ex.Message;
                LblError.Visible = true;
            }
        }
    }
}