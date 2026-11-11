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
        List<Usuario> usuariosEmpresasPendientes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaUsuariosActivos("");
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

        // Falta gestionar los try catch
        private void CargarGrillaUsuariosActivos(string filtro)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            usuariosActivos = negocio.ListarActivos();

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

            LblCantidadUsuariosActivos.Text = usuariosActivos.Count.ToString();
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
        protected void GrillaUsuarioActivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (e.CommandName == "Desactivar")
            {
                long idUsuario = Convert.ToInt64(e.CommandArgument);
                negocio.DesactivarUsuario(idUsuario);
            }
            else if (e.CommandName == "Editar")
            {
                long idUsuario = Convert.ToInt64(e.CommandArgument);
                // buscamos el usuario y cargamos sus datos en el formulario
                Usuario usuario = negocio.ListarActivos().FirstOrDefault(u => u.ID_Usuario == idUsuario);

                if (usuario != null)
                {
                    HfIdUsuario.Value = usuario.ID_Usuario.ToString();
                    TxtNombreUsuario.Text = usuario.Nombre;
                    TxtEmailUsuario.Text = usuario.Email;
                    DdlTipoUsuario.SelectedValue = usuario.TipoUsuario;

                    LblErrorUsuario.Visible = false;
                    PnlEditarUsuario.Visible = true;
                }
            }

            // Recargamos la grilla: el usuario sale de activos
            CargarGrillaUsuariosActivos(TxtBuscar.Text.Trim());
        }

        // validacion con ayuda de la IA para que se cargue siempre mail
        protected void BtnGuardarUsuario_Click(object sender, EventArgs e)
        {
            LblErrorUsuario.Visible = false;

            string nombre = TxtNombreUsuario.Text.Trim();
            string email = TxtEmailUsuario.Text.Trim();

            // validamos que no esten vacios
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email))
            {
                LblErrorUsuario.Text = "Por favor, completá el nombre y el correo electrónico.";
                LblErrorUsuario.Visible = true;
                return;
            }

            // validamos que el email tenga arroba y punto
            if (!email.Contains("@") || !email.Contains("."))
            {
                LblErrorUsuario.Text = "El correo electrónico no tiene un formato válido.";
                LblErrorUsuario.Visible = true;
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                long idUsuario = Convert.ToInt64(HfIdUsuario.Value);

                // validamos que ningun otro usuario tenga ese email
                if (negocio.ExisteEmail(email, idUsuario))
                {
                    LblErrorUsuario.Text = "Ya existe otro usuario con ese correo electrónico.";
                    LblErrorUsuario.Visible = true;
                    return;
                }

                Usuario usuario = new Usuario();
                usuario.ID_Usuario = idUsuario;
                usuario.Nombre = nombre;
                usuario.Email = email;
                usuario.TipoUsuario = DdlTipoUsuario.SelectedValue;

                negocio.Modificar(usuario);

                // ocultamos el formulario y recargamos la grilla
                PnlEditarUsuario.Visible = false;
                CargarGrillaUsuariosActivos(TxtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                LblErrorUsuario.Text = "Error al guardar el usuario: " + ex.Message;
                LblErrorUsuario.Visible = true;
            }
        }

        protected void BtnCancelarUsuario_Click(object sender, EventArgs e)
        {
            HfIdUsuario.Value = "";
            PnlEditarUsuario.Visible = false;
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

            if (e.CommandName == "Aceptar")
            {
                long idUsuario = Convert.ToInt64(e.CommandArgument);
                negocio.AprobarEmpresa(idUsuario);
            }
            else if (e.CommandName == "Rechazar")
            {
                long idUsuario = Convert.ToInt64(e.CommandArgument);
                negocio.RechazarEmpresa(idUsuario);
            }
            //recarga de grillas, sale de pendiente y se mueve a activos
            CargarGrillaEmpresasPendientes(TxtBuscarEmpresas.Text.Trim());
            CargarGrillaUsuariosActivos(TxtBuscar.Text.Trim());
        }

        protected void BtnVerBajas_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminBajas.aspx", false);
        }
        protected void BtnMateriales_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdminMateriales.aspx", false);
        }
    }
}