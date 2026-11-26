using Dominio;
using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                CargarGrilla();  //aca llamada al metodo sin filtro ; 
            }



        }

        // metodo para cargar la grilla y acepte un filtro ; 
        private void CargarGrilla(string filtro ="")
        {

            try
            {
                ReseñaNegocio negocio = new ReseñaNegocio();
                List<Reseña> lista;

                // GvResenias.DataBind();

                // Traemos las reseñas (activas o eliminadas según el check)
                if (chkVerEliminadas.Checked)
                {
                    lista = negocio.ListarPorEliminadas();
                }
                else
                {
                    lista = negocio.ListarTodas();
                }
                
                // Si el user escribio en el buscador 
                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    filtro = filtro.ToLower(); //pasamos a miniscula ; 

                    lista = lista.Where(r =>
                (r.Comentario != null && r.Comentario.ToLower().Contains(filtro)) ||
                (r.NombreUsuario != null && r.NombreUsuario.ToLower().Contains(filtro)) ).ToList();
                }

                GvResenias.DataSource = lista;
                GvResenias.DataBind();


                if (GvResenias.Columns.Count > 6)
                {
                    GvResenias.Columns[6].Visible = true; 
                }
                // if (GvResenias.Columns.Count > 6)
                //{
                //   GvResenias.Columns[6].Visible = chkVerEliminadas.Checked; // Ocultar la sexta columna (Ver motivo elim  )
                // }

            }

            catch (Exception ex)
            {
               lblError.Text = "Error al cargar: " + ex.Message;
                lblError.Visible = true;
            }

        }



        protected void GvResenias_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
           
                try
                {

                    ReseñaNegocio negocio = new ReseñaNegocio();

                    if (e.CommandName == "EliminarResenia")
                    {
                        long idResenia = Convert.ToInt64(e.CommandArgument);

                        string motivo = hfMotivoEliminar.Value;

                        negocio.Eliminar(idResenia, motivo);
                    }
                    else if (e.CommandName == "RestaurarResenia") // 
                    {
                        long idResenia = Convert.ToInt64(e.CommandArgument);
                        negocio.Restaurar(idResenia);
                    }

                    CargarGrilla(TxtBuscar.Text.Trim()); // Refresh manteniendo el filtro actual
                }
                catch (Exception ex)
                {
                    lblError.Text = "Error: " + ex.Message;
                    lblError.Visible = true;
                }


         }

        

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminCentral.aspx", false);
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            //pasamos el texto q escribieron ; 
            CargarGrilla(TxtBuscar.Text.Trim());
        }

        //  Actualizamos el checkbox para q mantenga la busqueda ; 
        protected void chkVerEliminadas_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla(TxtBuscar.Text.Trim());
        }

    }
}