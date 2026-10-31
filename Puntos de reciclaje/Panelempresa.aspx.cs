using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Puntos_de_reciclaje.Negocio;
using Puntos_de_reciclaje.Dominio;


namespace Puntos_de_reciclaje
{
    public partial class PanelEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               lblNombreEmpresa.Text = Convert.ToString(Session["NombreEmpresa"]);

                cargarPuntos(); 
            }


        }

        private void cargarPuntos()
        {

            try
            {
                Puntonegocio negocio = new Puntonegocio();
                long idEmpresa = Convert.ToInt64(Session["IdEmpresa"] ?? 1);

                List<Puntoreciclaje> lista = negocio.ListarporEmpresa(idEmpresa);

            gvPuntos.DataSource = lista;
            gvPuntos.DataBind();

            }
            catch (Exception ex)
            {
                lblNombreEmpresa.Text = "Error #404 " + ex.Message;
            }
        }

        protected void btnNuevoPunto_Click(object sender, EventArgs e)
        {
          //   Response.Redirect("NuevoPunto.aspx");
        }
    }
    
         }