using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puntos_de_reciclaje.PaginasAdmin
{
    public partial class PanelAdminCentral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnUsuariosActivos_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminUsuariosActivos.aspx", false);
        }

        protected void BtnEmpresasPendientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminEmpresasPendientes.aspx", false);
        }

        protected void BtnAdministrarMateriales_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminMateriales.aspx", false);
        }

        protected void BtnRegistros_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminRegistros.aspx", false);
        }

        protected void BtnAdministrarResenias_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminResenias.aspx", false);
        }



    }
}