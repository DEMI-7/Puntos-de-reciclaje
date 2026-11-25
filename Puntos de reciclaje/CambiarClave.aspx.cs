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
    public partial class CambiarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // para cambiar clave si o si tiene que estar logueado
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

        }

        protected void BtnCambiarClave_Click(object sender, EventArgs e)
        {
            LblError.Visible = false;
            LblExito.Visible = false;

            string claveActual = TxtClaveActual.Text;
            string claveNueva = TxtClaveNueva.Text;
            string repetirClave = TxtRepetirClave.Text;

            // validamos que no haya campos vacios
            if (string.IsNullOrWhiteSpace(claveActual) || string.IsNullOrWhiteSpace(claveNueva) || string.IsNullOrWhiteSpace(repetirClave))
            {
                LblError.Text = "Por favor complete todos los campos";
                LblError.Visible = true;
                return;
            }


            if (claveNueva != repetirClave)
            {
                LblError.Text = "Las claves nuevas no son iguales";
                LblError.Visible = true;
                return;
            }

            if (claveNueva == claveActual)
            {
                LblError.Text = "La nueva clave tiene que ser distinta de la actual.";
                LblError.Visible = true;
                return;
            }

            try
            {
                Usuario usuarioSesion = (Usuario)Session["usuario"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                // verificamos la clave actual reutilizando Loguear con el email de la sesion
                Usuario verificacion = new Usuario();
                verificacion.Email = usuarioSesion.Email;
                verificacion.Contraseña = claveActual;

                if (!negocio.Loguear(verificacion))
                {
                    LblError.Text = "La clave actual es incorrecta";
                    LblError.Visible = true;
                    return;
                }

                negocio.CambiarClave(usuarioSesion.ID_Usuario, claveNueva);

                LblExito.Text = "Su clave se cambió correctamente";
                LblExito.Visible = true;
            }
            catch (Exception ex)
            {
                LblError.Text = "Error al cambiar la clave: " + ex.Message;
                LblError.Visible = true;
            }
        }
    }
}