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
    public partial class CrearPunto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               CargarMateriales();
            }

        }
       //  revisar ; 
        private void CargarMateriales()
        {

            MaterialNegocio negocio = new MaterialNegocio();

            try
            {
                List<Material> lista = negocio.listar();

                cblMateriales.DataSource = lista;

                cblMateriales.DataValueField = "Nombre";

                cblMateriales.DataTextField = "IdMaterial";

                cblMateriales.DataBind();
            }
            catch (Exception ex)
            {
               //  lblError.Text = "Error #404 " + ex.Message;
            }
            
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            lblError.Visible = false;

            try
            {
                if (string.IsNullOrWhiteSpace (txtDireccion.Text)|| string.IsNullOrWhiteSpace(HfLatitud.Value) )
                {
                    lblError.Text = "Por favor, ingresa una direccion y selecciona una ubicacion en el mapa. Puto.";
                    lblError.Visible = true;

                    return; 
                }
                PuntoReciclaje nuevoPunto = new PuntoReciclaje();
                nuevoPunto.IdUsuarioCreador = Convert.ToInt64(Session["IdEmpresa"] ?? 1);

           
                nuevoPunto.Horario = txtHorario.Text;

                nuevoPunto.Direccion = txtDireccion.Text;
                nuevoPunto.Latitud = Convert.ToDecimal(HfLatitud.Value);

                nuevoPunto.Longitud = Convert.ToDecimal(HfLongitud.Value);

              PuntoNegocio negocioPuntobich = new PuntoNegocio();

                long idPuntogenerado = negocioPuntobich.Agregar(nuevoPunto);

                foreach (ListItem xx in cblMateriales.Items)
                  
                {
                    if (  xx.Selected )
                    {
                        long idMaterial = Convert.ToInt64(xx.Value); 
                        negocioPuntobich.AgregarMaterialalPunto(idPuntogenerado, idMaterial);
                    }
                }
                //redirigimos hacia panel empresa.aspx ; 
                Response.Redirect("PanelEmpresa.aspx");
            }
            catch (Exception ex)
            {
                 lblError.Text = "Error #404 " + ex.Message;
                 lblError.Visible = true;
            }
        }
    }
}