using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Puntos_de_reciclaje.PaginasAdmin
{
    public partial class PanelAdminResenias : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // si no puede ver el panel admin, lo mandamos a la pagina de error
            if (!Seguridad.PuedeVerPanelAdmin(Session["usuario"]))
            {
                Response.Redirect("~/Error.aspx");
            }

            if  (!IsPostBack)
            {
                CargarGrilla();
            }



        }


        private void CargarGrilla()
        {

            try
            {
                Negocio.ReseñaNegocio negocio = new Negocio.ReseñaNegocio();
                GvResenias.DataSource = negocio.ListarTodas();

                GvResenias.DataBind();
            }
            catch (Exception ex)
            {
               lblError.Text = "Error al cargar: " + ex.Message;
                lblError.Visible = true;
            }

        }


        protected void GvResenias_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
           if (e.CommandName=="EliminarResenia")
            {
                try
                {

                    long idResenia = Convert.ToInt64(e.CommandArgument);

                    String motivo = hfMotivoEliminar.Value; // asignamos el motivo ; 


                    ReseñaNegocio negocio = new ReseñaNegocio();

                    negocio.Eliminar(idResenia, motivo);

                    CargarGrilla(); // refrescamos para q desaparezca ; 

                }
                catch (Exception ex)
                {
                    lblError.Text = "Error al eliminar: " + ex.Message;
                    lblError.Visible = true;

                }


            }

        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminCentral.aspx", false);
        }

    }
}