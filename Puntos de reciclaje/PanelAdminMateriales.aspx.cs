using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puntos_de_reciclaje
{
    public partial class PanelAdminMateriales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaMateriales();
            }
        }

        private void CargarGrillaMateriales()
        {
            MaterialNegocio negocio = new MaterialNegocio();
            List<Material> lista = negocio.listar();

            GrillaMateriales.DataSource = lista;
            GrillaMateriales.DataBind();

            LblCantidadMateriales.Text = lista.Count.ToString();
        }

        protected void GrillaMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // cambiar indice de pagina
            GrillaMateriales.PageIndex = e.NewPageIndex;
            CargarGrillaMateriales();
        }
        protected void GrillaMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MaterialNegocio negocio = new MaterialNegocio();

            if (e.CommandName == "Editar")
            {
                long idMaterial = Convert.ToInt64(e.CommandArgument);

                // buscamos el material en la lista y cargamos sus datos en el formulario
                Material material = negocio.listar().FirstOrDefault(m => m.IdMaterial == idMaterial);

                if (material != null)
                {
                    HfIdMaterial.Value = material.IdMaterial.ToString();
                    TxtNombre.Text = material.Nombre;
                    TxtInfo.Text = material.Info;
                    LblTituloFormulario.Text = "Modificar material";
                }
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            LblError.Visible = false;

            // validamos que el nombre no este vacio
            if (string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                LblError.Text = "Por favor, ingresá el nombre del material.";
                LblError.Visible = true;
                return;
            }

            try
            {
                Material material = new Material();
                material.Nombre = TxtNombre.Text.Trim();
                material.Info = TxtInfo.Text.Trim();

                MaterialNegocio negocio = new MaterialNegocio();

                if (!string.IsNullOrEmpty(HfIdMaterial.Value))
                {
                    // editar: el formulario tiene un ID guardado
                    material.IdMaterial = Convert.ToInt64(HfIdMaterial.Value);
                    negocio.Modificar(material);
                }
                else
                {
                    // crear: el formulario no tiene ID
                    negocio.Agregar(material);
                }

                LimpiarFormulario();
                CargarGrillaMateriales();
            }
            catch (Exception ex)
            {
                LblError.Text = "Error al guardar el material: " + ex.Message;
                LblError.Visible = true;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            HfIdMaterial.Value = "";
            TxtNombre.Text = "";
            TxtInfo.Text = "";
            LblTituloFormulario.Text = "Nuevo material";
            LblError.Visible = false;
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdministrador.aspx", false);
        }
    }
}