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
            if (string.IsNullOrWhiteSpace(TxtEmail.Text) || string.IsNullOrWhiteSpace(TxtContraseña.Text))
            {
                return; 
            }

            //soimulacion de login , sin base de datos; 
            Session["IdEmpresa"] = 1;
            Session["NombreEmpresa"] = "EcoRecicla S.A. (emp prueba )";

            // lo mandamos al panel de la empresa
            Response.Redirect("PanelEmpresa.aspx");
        }
    }
}