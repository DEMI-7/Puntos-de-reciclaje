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
    public partial class _Default : Page
    {
        private List<PuntoReciclaje> listaPuntos = new List<PuntoReciclaje>();
        public string ListaPuntosJson {  get; set; }
        private List<Material> listaMateriales =new List<Material>();
        public string ListaMaterialesJson { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Listamos los puntos con la conexion directa a la DB
                PuntoNegocio negocio = new PuntoNegocio();

                // Falta Gestionar posible error con try catch
                listaPuntos = negocio.listar();

                // Traducimos la lista a formato json asi el mapa puede dibujarlo con el javascript
                ListaPuntosJson = Newtonsoft.Json.JsonConvert.SerializeObject(listaPuntos);

                MaterialNegocio negocioMaterial = new MaterialNegocio();
                listaMateriales = negocioMaterial.listar();
                ListaMaterialesJson = Newtonsoft.Json.JsonConvert.SerializeObject(listaMateriales);
            }
        }

    }
}