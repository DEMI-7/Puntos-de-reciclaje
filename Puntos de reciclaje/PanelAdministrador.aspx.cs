using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Puntos_de_reciclaje
{
    public partial class PanelAdmintrador : System.Web.UI.Page
    {
        List<Usuario> usuariosActivos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaUsuariosActivos("");
            }

        }

        // Falta gestionar los try catch
        private void CargarGrillaUsuariosActivos(string filtro)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            usuariosActivos = negocio.listarActivos();

            if (!string.IsNullOrEmpty(filtro))
            {
                usuariosActivos = usuariosActivos.Where(u =>
                (u.Nombre != null && u.Nombre.ToLower().Contains(filtro.ToLower())) ||
                (u.Email != null && u.Email.ToLower().Contains(filtro.ToLower()))
                ).ToList();
            }

            // controlar si la lista sale vacia
            if (usuariosActivos.Count > 0)
            {
                GrillaUsuarioActivos.DataSource = usuariosActivos;
                GrillaUsuarioActivos.DataBind();
                GrillaUsuarioActivos.Visible = true;
                LblSinResultados.Visible = false;
            }
            else
            {
                GrillaUsuarioActivos.Visible = false;
                LblSinResultados.Visible = true;
            }

            LblCantidadUsuarios.Text = usuariosActivos.Count.ToString();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Volvemos a cargar la grilla pero pasandole el contenido del textbox
            CargarGrillaUsuariosActivos(TxtBuscar.Text.Trim());
            GrillaUsuarioActivos.PageIndex = 0;
        }

        protected void GrillaUsuarioActivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // cambiar indice de pagina
            GrillaUsuarioActivos.PageIndex = e.NewPageIndex;
            CargarGrillaUsuariosActivos(TxtBuscar.Text.Trim());
        }
    }
}