using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puntos_de_reciclaje.PaginasAdmin
{
    public partial class PanelAdminRegistros : System.Web.UI.Page
    {
        List<RegistroAccion> listaRegistros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarGrillaRegistros(string.Empty);
            }
        }

        private void CargarGrillaRegistros(string filtro)
        {
            RegistroAccionNegocio negocio = new RegistroAccionNegocio();
            listaRegistros = negocio.Listar();

            if (!string.IsNullOrEmpty(filtro))
            {
                listaRegistros = listaRegistros.Where(r =>
                (r.Admin.Nombre != null && r.Admin.Nombre.ToLower().Contains(filtro.ToLower())) ||
                (r.UsuarioAfectado.Nombre != null && r.UsuarioAfectado.Nombre.ToLower().Contains(filtro.ToLower()))
                ).ToList();
            }

            // controlar si la lista sale vacia
            if (listaRegistros.Count > 0)
            {
                GrillaRegistros.DataSource = listaRegistros;
                GrillaRegistros.DataBind();
                GrillaRegistros.Visible = true;
                LblSinResultados.Visible = false;
            }
            else
            {
                GrillaRegistros.Visible = false;
                LblSinResultados.Visible = true;
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {

        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void GrillaRegistros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GrillaRegistros_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}