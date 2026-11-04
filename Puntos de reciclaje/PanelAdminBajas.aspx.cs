using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Puntos_de_reciclaje
{
    public partial class PanelAdminBajas : System.Web.UI.Page
    {
        List<Usuario> usuariosDesactivados;
        List<Usuario> empresasRechazadas;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaUsuariosDesactivados("");
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

        private void CargarGrillaUsuariosDesactivados(string filtro)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            usuariosDesactivados = negocio.ListarDesactivados();

            if (!string.IsNullOrEmpty(filtro))
            {
                usuariosDesactivados = usuariosDesactivados.Where(u =>
                (u.Nombre != null && u.Nombre.ToLower().Contains(filtro.ToLower())) ||
                (u.Email != null && u.Email.ToLower().Contains(filtro.ToLower()))
                ).ToList();
            }

            // controlar si la lista sale vacia
            if (usuariosDesactivados.Count > 0)
            {
                GrillaUsuarioDesactivados.DataSource = usuariosDesactivados;
                GrillaUsuarioDesactivados.DataBind();
                GrillaUsuarioDesactivados.Visible = true;
                LblSinResultados.Visible = false;
            }
            else
            {
                GrillaUsuarioDesactivados.Visible = false;
                LblSinResultados.Visible = true;
            }

            LblCantidadUsuariosDesactivados.Text = usuariosDesactivados.Count.ToString();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            // Volvemos a cargar la grilla pero pasandole el contenido del textbox
            CargarGrillaUsuariosDesactivados(TxtBuscar.Text.Trim());
            GrillaUsuarioDesactivados.PageIndex = 0;
        }

        protected void BtnBuscarEmpresas_Click(object sender, EventArgs e)
        {
            CargarGrillaEmpresasRechazadas(TxtBuscarEmpresas.Text.Trim());
            GrillaEmpresasRechazadas.PageIndex = 0;
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdministrador.aspx");
        }

        protected void GrillaEmpresasRechazadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrillaEmpresasRechazadas.PageIndex = e.NewPageIndex;
            CargarGrillaEmpresasRechazadas(TxtBuscarEmpresas.Text.Trim());
        }

        protected void GrillaUsuarioDesactivados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // cambiar indice de pagina
            GrillaUsuarioDesactivados.PageIndex = e.NewPageIndex;
            CargarGrillaUsuariosDesactivados(TxtBuscar.Text.Trim());
        }
    }
}