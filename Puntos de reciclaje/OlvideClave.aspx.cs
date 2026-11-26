using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puntos_de_reciclaje
{
    public partial class OlvideClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnRecuperar_Click(object sender, EventArgs e)
        {
            LblError.Visible = false;
            LblExito.Visible = false;

            string email = TxtEmail.Text.Trim();
            string palabra1 = TxtPalabra1.Text.Trim().ToLower();
            string palabra2 = TxtPalabra2.Text.Trim().ToLower();
            string claveNueva = TxtClaveNueva.Text;
            string repetirClave = TxtRepetirClave.Text;

            // validamos que no haya campos vacios
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(palabra1) || string.IsNullOrWhiteSpace(palabra2) ||
                string.IsNullOrWhiteSpace(claveNueva) || string.IsNullOrWhiteSpace(repetirClave))
            {
                LblError.Text = "Por favor complete todos los campos";
                LblError.Visible = true;
                return;
            }

            if (claveNueva != repetirClave)
            {
                LblError.Text = "Las claves nuevas no coinciden";
                LblError.Visible = true;
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                long idUsuario = negocio.VerificarPalabras(email, palabra1, palabra2);

                // mismo mensaje si no existe el email o si las palabras estan mal
                if (idUsuario == 0)
                {
                    LblError.Text = "Los datos no coinciden. Revise el correo y las palabras de recuperación.";
                    LblError.Visible = true;
                    return;
                }

                negocio.CambiarClave(idUsuario, claveNueva);

                // limpiamos y avisamos
                TxtEmail.Text = "";
                TxtPalabra1.Text = "";
                TxtPalabra2.Text = "";
                LblExito.Text = "Su clave se ha cambiado con exito. ";
                LblExito.Visible = true;
            }
            catch (Exception ex)
            {
                LblError.Text = "Error al recuperar la clave: " + ex.Message;
                LblError.Visible = true;
            }
        }
    }
}