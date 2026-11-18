using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Puntos_de_reciclaje
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // si hay alguien logueado mostramos su nombre en la barra
            Usuario usuario = (Usuario)Session["usuario"];

            if (usuario != null)
            {
                LblUsuarioLogueado.Text = usuario.Nombre;
                LblUsuarioLogueado.Visible = true;
                BtnCerrarSesion.Visible = true;
                LiIniciarSesion.Visible = false;
            }
            else
            {
                LblUsuarioLogueado.Visible = false;
                BtnCerrarSesion.Visible = false;
                LiIniciarSesion.Visible = true;
            }

        }
        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            // borramos todo lo guardado en la sesion y volvemos al mapa
            Session.Clear();
            Response.Redirect("~/Default.aspx", false);
        }
    }
}