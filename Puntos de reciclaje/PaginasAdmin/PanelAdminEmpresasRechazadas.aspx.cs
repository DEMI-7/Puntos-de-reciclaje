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
    public partial class PanelAdminEmpresasRechazadas : System.Web.UI.Page
    {

        List<Usuario> empresasRechazadas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaEmpresasRechazadas("");
            }
        }

        // Falta gestionar los try catch
        private void CargarGrillaEmpresasRechazadas(string filtro)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            empresasRechazadas = negocio.ListarEmpresasRechazadas();

            if (!string.IsNullOrEmpty(filtro))
            {
                empresasRechazadas = empresasRechazadas.Where(u =>
                (u.Nombre != null && u.Nombre.ToLower().Contains(filtro.ToLower())) ||
                (u.Email != null && u.Email.ToLower().Contains(filtro.ToLower()))
                ).ToList();
            }

            // controlar si la lista sale vacia
            if (empresasRechazadas.Count > 0)
            {
                GrillaEmpresasRechazadas.DataSource = empresasRechazadas;
                GrillaEmpresasRechazadas.DataBind();
                GrillaEmpresasRechazadas.Visible = true;
                LblSinResultadosEmpresasRechazadas.Visible = false;
            }
            else
            {
                GrillaEmpresasRechazadas.Visible = false;
                LblSinResultadosEmpresasRechazadas.Visible = true;
            }

            LblCantidadEmpresasRechazadas.Text = empresasRechazadas.Count.ToString();
        }
        protected void BtnBuscarEmpresas_Click(object sender, EventArgs e)
        {
            CargarGrillaEmpresasRechazadas(TxtBuscarEmpresas.Text.Trim());
            GrillaEmpresasRechazadas.PageIndex = 0;
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminEmpresasPendientes.aspx");
        }

        protected void GrillaEmpresasRechazadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrillaEmpresasRechazadas.PageIndex = e.NewPageIndex;
            CargarGrillaEmpresasRechazadas(TxtBuscarEmpresas.Text.Trim());
        }
        protected void GrillaEmpresasRechazadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (e.CommandName == "Pendiente")
            {
                long idUsuario = Convert.ToInt64(e.CommandArgument);
                negocio.MandarAPendiente(idUsuario);
            }
            //recarga la grilla para sacar a la empresa de rechazada 
            CargarGrillaEmpresasRechazadas(TxtBuscarEmpresas.Text.Trim());
        }
    }
}