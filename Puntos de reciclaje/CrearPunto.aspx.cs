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

                cblMateriales.DataValueField = "IdMaterial";

                cblMateriales.DataTextField = "Nombre";

                cblMateriales.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error #404 " + ex.Message;
            }
            
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            lblError.Visible = false;




            string latStr = Request.Form["latitudMapa"];
            string lngStr = Request.Form["longitudMapa"];

           

           
                if (string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(latStr))
                {
                    //   lblError.Text = "Dirección: " + txtDireccion.Text + " | Latitud: " + HfLatitud.Value;
                    lblError.Text = "Por favor, ingresa una direccion y selecciona una ubicacion en el mapa.";
                    lblError.Visible = true;

                    return;
                }
                try
                {

                    // creamos el objeto ; 

                    PuntoReciclaje nuevoPunto = new PuntoReciclaje();
                nuevoPunto.IdUsuarioCreador = Convert.ToInt64(Session["IdEmpresa"] ?? 1);

           
                nuevoPunto.Horario = txtHorario.Text;

                nuevoPunto.Direccion = txtDireccion.Text;


                // Usamos CultureInfo.InvariantCulture para que C# entienda que el punto es decimal
                System.Globalization.CultureInfo cultura = System.Globalization.CultureInfo.InvariantCulture;

                nuevoPunto.Latitud = Math.Round(Convert.ToDecimal(latStr.Replace(',', '.'), cultura), 8);
                nuevoPunto.Longitud = Math.Round(Convert.ToDecimal(lngStr.Replace(',', '.'), cultura), 8);

                //guardamos el punto 

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='PanelEmpresa.aspx';", true);
            }
            catch (Exception ex)
            {
                 lblError.Text = "Error #404 " + ex.Message;
                 lblError.Visible = true;
            }
        }
    }
}