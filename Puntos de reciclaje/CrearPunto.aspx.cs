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

                if (Request.QueryString["id"] != null)
                {
                    long idPunto = Convert.ToInt64(Request.QueryString["id"]);
                    Cargardatosparaeditar(idPunto);
                    btnGuardar.Text = "Actualizar Punto de Reciclaje";

                }
            }

        }

        private void Cargardatosparaeditar(long idPunto)
        {
            PuntoNegocio negocio = new PuntoNegocio();
            try
            {
                long idEmpresa = Convert.ToInt64(Session["IdEmpresa"] ?? 1);
                var lista = negocio.listar();
                var punto = lista.FirstOrDefault(p => p.IdPunto == idPunto && p.IdUsuarioCreador == idEmpresa);

                if (punto != null)
                {
                    txtDireccion.Text = punto.Direccion;
                    txtHorario.Text = punto.Horario;
                    // marcamos los materiales que ya tiene el punto
                    foreach (ListItem xx in cblMateriales.Items)
                    {
                        long idMaterial = Convert.ToInt64(xx.Value);
                        if (punto.ListaMateriales.Any(m => m.IdMaterial == idMaterial))
                        {
                            xx.Selected = true;
                        }
                    }

                    // formateamos los decimales con punto para  java  segun ia ; 
                    string lat = punto.Latitud.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    string lng = punto.Longitud.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    // mandamoss un script para mover el mapa a la ubicación del punto

                    string script = $@"
                var pLat = parseFloat('{lat}');
                var pLng = parseFloat('{lng}');
                document.getElementById('latitudMapa').value = pLat;
                document.getElementById('longitudMapa').value = pLng;
                moverMapa(pLat, pLng);
            ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarMapa", script, true);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al cargar los datos: " + ex.Message;
                lblError.Visible = true;
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

                long idPuntogenerado;

                if (Request.QueryString["id"] != null)
                {
                    // En editar actualizamos el punto y borramos sus materiales anteriores
                    nuevoPunto.IdPunto = Convert.ToInt64(Request.QueryString["id"]);
                    negocioPuntobich.Modificar(nuevoPunto);
                    negocioPuntobich.EliminarMaterialesDelPunto(nuevoPunto.IdPunto);

                    idPuntogenerado = nuevoPunto.IdPunto;
                }
                else
                {
                    // En crear, insertamos el punto nuevo
                    idPuntogenerado = negocioPuntobich.Agregar(nuevoPunto);
                }

                // En ambos casos guardamos los materiales tildados
                foreach (ListItem xx in cblMateriales.Items)
                {
                    if (xx.Selected)
                    {
                        long idMaterial = Convert.ToInt64(xx.Value);
                        negocioPuntobich.AgregarMaterialalPunto(idPuntogenerado, idMaterial);
                    }
                }

                // Lo redirigimos hacia panel empresa.aspx 
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