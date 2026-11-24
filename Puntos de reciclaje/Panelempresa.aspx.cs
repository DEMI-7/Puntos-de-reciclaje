using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocio;
using Dominio;


namespace Puntos_de_reciclaje
{
    public partial class PanelEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // primero vemos si no puede ver el panel de empresa para mandarlo a la pagina de error
            if (!Seguridad.PuedeVerPanelEmpresa(Session["usuario"]))
            {
                Response.Redirect("~/Error.aspx");
            }
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario != null)
                {
                    lblNombreEmpresa.Text = usuario.Nombre;
                }
                cargarPuntos(); 
            }


        }

        private void cargarPuntos()
        {

            try
            {
                PuntoNegocio negocio = new PuntoNegocio();
                Usuario usuario = (Usuario)Session["usuario"];
                long idEmpresa = usuario != null ? usuario.ID_Usuario : 1;

                List<PuntoReciclaje> lista = negocio.ListarporEmpresa(idEmpresa);

            gvPuntos.DataSource = lista;
            gvPuntos.DataBind();

            }
            catch (Exception ex)
            {
                lblError.Text = "Error al cargar los puntos: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void BtnNuevoPunto_Click(object sender, EventArgs e)
        {
          Response.Redirect("CrearPunto.aspx", false);
        }

        protected void GvPuntos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarPunto")
            {
                try
                {
                    long idPunto = Convert.ToInt64(e.CommandArgument);
                    PuntoNegocio negocio = new PuntoNegocio();

                    negocio.Eliminar(idPunto);

                    // Recargamos la lista de puntos después de eliminar
                    cargarPuntos();

                }
                catch (Exception ex)
                {
                    lblError.Text = "Error al eliminar el punto: " + ex.Message;
                    lblError.Visible = true;
                    

                }

            }





        }
    }
}


