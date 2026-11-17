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
            if (string.IsNullOrWhiteSpace(email))
            {
                LblError.Text = "Por favor, ingresá tu correo electrónico.";
                LblError.Visible = true;
                return;
            }
            // buscamos el usuario en la base (por ahora sin verificar la contraseña)
            try
            {
                Usuario usuario = new Usuario();
                usuario.Email = email;
                usuario.Contraseña = TxtContraseña.Text;
                UsuarioNegocio negocio = new UsuarioNegocio();
              
                if (!negocio.Loguear(usuario))
                {
                    LblError.Text = "Usuario inexistente";
                    LblError.Visible = true;
                    return;
                }

                // guardamos en la sesion el usuario que entro
                Session["IdEmpresa"] = usuario.ID_Usuario;
                Session["NombreEmpresa"] = usuario.Nombre;

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