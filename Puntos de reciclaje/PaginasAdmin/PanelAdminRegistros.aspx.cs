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
            PnlDetalleMovimiento.Visible = false;

            if (!IsPostBack)
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
            Response.Redirect("PanelAdminCentral.aspx", false);
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaRegistros(TxtBuscar.Text.Trim());
            GrillaRegistros.PageIndex = 0; // Reiniciar la paginacion al buscar
        }

        protected void GrillaRegistros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrillaRegistros.PageIndex = e.NewPageIndex;
            CargarGrillaRegistros(TxtBuscar.Text.Trim());
        }

        

        protected void GrillaRegistros_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "verdetalle")
            {
                CargarGrillaRegistros(string.Empty);

                long IdRegistro = Convert.ToInt64(e.CommandArgument);

                RegistroAccion registro = listaRegistros.Find(r => r.ID_Movimiento == IdRegistro);

                PnlDetalleMovimiento.Visible = true;
                LblIdRegistro.Text = "Id del registro: " + registro.ID_Movimiento;
                LblHora.Text = "Hora de la acción: " + registro.HoraMovimiento.ToString("dd/MM/yyyy HH:mm:ss");
                LblNombreAdmin.Text = registro.Admin.Nombre;
                LblMailAdmin.Text = registro.Admin.Email;
                LblNombreUsuario.Text = registro.UsuarioAfectado.Nombre;
                LblTipoCuenta.Text = registro.UsuarioAfectado.TipoUsuario;
                LblMailUsuario.Text = registro.UsuarioAfectado.Email;
                LblEstadoAnterior.Text = registro.EstadoAnterior;
                LblEstadoNuevo.Text = registro.EstadoNuevo;
                DivDetalle.InnerText = registro.Detalle;
            }
        }

        protected void BtnCerrarDetalle_Click(object sender, EventArgs e)
        {
            PnlDetalleMovimiento.Visible = false;
        }
    }
}