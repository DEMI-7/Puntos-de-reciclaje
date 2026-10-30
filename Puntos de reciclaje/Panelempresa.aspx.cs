using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puntos_de_reciclaje
{
    public partial class PanelEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //verificacion de q la emp este loggueada 
            
            if (Session["IdEmpresa"] == null)
            {
                //Response.Redirect("Login.aspx");
                return;
            }
            // 2. Solo cargamos datos la PRIMERA vez que entra a la página
            if (!IsPostBack)
            {
                // Mostramos el nombre que guardamos en el Login
                //LblNombreEmpresa.Text = Convert.ToString(Session["NombreEmpresa"]);
                // Cargamos los datos de la grilla
                CargarPuntos();
            }
        }

        private void CargarPuntos()
        {
            // Simulacion de datos 
            // Más adelante, esta lista va a venir de sql ; 
            var listaSimulada = new List<object>
            {
                new { Id = 1, Direccion = "Av. San Martín 123, José C. Paz", TipoMaterial = "Plástico y Vidrio" },
                new { Id = 2, Direccion = "Belgrano 456, José C. Paz", TipoMaterial = "Cartón y Papel" },
                new { Id = 3, Direccion = "Ruta 24 Km 5", TipoMaterial = "Electrónicos" }
            };

            //GvPuntos.DataSource = listaSimulada;
            //GvPuntos.DataBind(); 
        }

        protected void BtnNuevoPunto_Click(object sender, EventArgs e)
        {
            // Cuando creemos la página para agregar puntos
            // Response.Redirect("CrearPunto.aspx");
        }
    }

    }
