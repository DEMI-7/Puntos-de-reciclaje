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
    public partial class PanelAdminEmpresasPendientes : System.Web.UI.Page
    {
        List<Usuario> usuariosEmpresasPendientes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaEmpresasPendientes("");
            }
        }

        // Falta gestionar los try catch
        private void CargarGrillaEmpresasPendientes(string filtro)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            usuariosEmpresasPendientes = negocio.ListarEmpresasPendientes();

            if (!string.IsNullOrEmpty(filtro))
            {
                usuariosEmpresasPendientes = usuariosEmpresasPendientes.Where(u =>
                (u.Nombre != null && u.Nombre.ToLower().Contains(filtro.ToLower())) ||
                (u.Email != null && u.Email.ToLower().Contains(filtro.ToLower()))
                ).ToList();
            }

            // controlar si la lista sale vacia
            if (usuariosEmpresasPendientes.Count > 0)
            {
                GrillaEmpresasPendientes.DataSource = usuariosEmpresasPendientes;
                GrillaEmpresasPendientes.DataBind();
                GrillaEmpresasPendientes.Visible = true;
                LblSinResultadosEmpresasPendientes.Visible = false;
            }
            else
            {
                GrillaEmpresasPendientes.Visible = false;
                LblSinResultadosEmpresasPendientes.Visible = true;
            }

            LblCantidadEmpresasPendientes.Text = usuariosEmpresasPendientes.Count.ToString();
        }

        protected void GrillaEmpresasPendientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // cambiar indice de pagina
            GrillaEmpresasPendientes.PageIndex = e.NewPageIndex;
            CargarGrillaEmpresasPendientes(TxtBuscarEmpresas.Text.Trim());
        }

        protected void BtnBuscarEmpresas_Click(object sender, EventArgs e)
        {
            // Volvemos a cargar la grilla pero pasandole el contenido del textbox
            CargarGrillaEmpresasPendientes(TxtBuscarEmpresas.Text.Trim());
            GrillaEmpresasPendientes.PageIndex = 0;
        }
        protected void GrillaEmpresasPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();


            long IdEmpresaAfectada = Convert.ToInt64(e.CommandArgument);

            RegistroAccion registroNuevo = new RegistroAccion();

            // ES NECESARIO OBTENER EL ID DESDE LA SESSION
            registroNuevo.Admin.ID_Usuario = 1; // Esta HARCODEADO
            registroNuevo.UsuarioAfectado.ID_Usuario = IdEmpresaAfectada;
            registroNuevo.EstadoAnterior = "pendiente";
            registroNuevo.Detalle = HfDetalleRegistro.Value.Trim();

            if (e.CommandName == "Aceptar")
            {
                registroNuevo.EstadoNuevo = "aprobado";
                negocio.AprobarEmpresa(registroNuevo);
            }
            else if (e.CommandName == "Rechazar")
            {
                registroNuevo.EstadoNuevo = "rechazado";
                negocio.RechazarEmpresa(registroNuevo);
            }

            //recarga de grillas, sale de pendiente y se mueve a activos
            CargarGrillaEmpresasPendientes(TxtBuscarEmpresas.Text.Trim());
            
        }

        protected void BtnVerBajas_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminEmpresasRechazadas.aspx", false);
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminCentral.aspx", false);

        }
    }
}