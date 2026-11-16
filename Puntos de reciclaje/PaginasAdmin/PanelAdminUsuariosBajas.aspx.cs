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
    public partial class PanelAdminUsuariosBajas : System.Web.UI.Page
    {
        List<Usuario> usuariosDesactivados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaUsuariosDesactivados("");
                
            }
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

       

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminUsuariosActivos.aspx");
        }

        

        protected void GrillaUsuarioDesactivados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // cambiar indice de pagina
            GrillaUsuarioDesactivados.PageIndex = e.NewPageIndex;
            CargarGrillaUsuariosDesactivados(TxtBuscar.Text.Trim());
        }
        protected void GrillaUsuarioDesactivados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            RegistroAccion registroNuevo = new RegistroAccion();

            // ES NECESARIO OBTENER EL ID DESDE LA SESSION
            registroNuevo.Admin.ID_Usuario = 1; // Esta HARCODEADO
            registroNuevo.Detalle = HfDetalleRegistro.Value.Trim();


            if (e.CommandName == "Activar")
            {
                long idUsuario = Convert.ToInt64(e.CommandArgument);
                registroNuevo.UsuarioAfectado.ID_Usuario = idUsuario;
                registroNuevo.EstadoAnterior = "INACTIVO";
                registroNuevo.EstadoNuevo = "ACTIVO";
                negocio.ActivarUsuario(registroNuevo);
            }
            else if (e.CommandName == "Pendiente")
            {
                long idUsuario = Convert.ToInt64(e.CommandArgument);
                registroNuevo.UsuarioAfectado.ID_Usuario = idUsuario;
                registroNuevo.EstadoAnterior = "INACTIVO";
                registroNuevo.EstadoNuevo = "pendiente";
                negocio.MandarAPendiente(registroNuevo);
            }

            // Recargamos la grilla: el usuario sale de desactivados
            CargarGrillaUsuariosDesactivados(TxtBuscar.Text.Trim());
        }
    }
}