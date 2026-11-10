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
    public partial class PanelAdminMateriales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaMateriales();
            }
        }

        private void CargarGrillaMateriales()
        {
            MaterialNegocio negocio = new MaterialNegocio();
            List<Material> lista = negocio.listar();

            GrillaMateriales.DataSource = lista;
            GrillaMateriales.DataBind();

            LblCantidadMateriales.Text = lista.Count.ToString();
        }

        protected void GrillaMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // cambiar indice de pagina
            GrillaMateriales.PageIndex = e.NewPageIndex;
            CargarGrillaMateriales();
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdministrador.aspx", false);
        }
    }
}