using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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

                PuntoNegocio negocio = new PuntoNegocio();

                // Falta Gestionar posible error con try catch
                listaPuntos = negocio.listar();

                // cargamos las reseñas para cada punto ; 
                ReseñaNegocio negocioReseña = new ReseñaNegocio();
                foreach (var punto in listaPuntos)
                {
                    punto.Resenias = negocioReseña.ListarPorPunto(punto.IdPunto);
                }

                

                // Traducimos la lista a formato json asi el mapa puede dibujarlo con el javascript
                ListaPuntosJson = Newtonsoft.Json.JsonConvert.SerializeObject(listaPuntos);

                MaterialNegocio negocioMaterial = new MaterialNegocio();
                listaMateriales = negocioMaterial.listar();
                ListaMaterialesJson = Newtonsoft.Json.JsonConvert.SerializeObject(listaMateriales);

                if (Session["usuario"] != null)
                {
                    // Si esta logueado, mostramos el form y guardamos el  ID en el HiddenField ; 
                    Dominio.Usuario xx = (Dominio.Usuario)Session["usuario"];
                    hfIdUsuario.Value = xx.ID_Usuario.ToString();

                    pnlFormularioResena.Visible = true;
                    pnlMensajeLogin.Visible = false;
                }
                else
                { // si  no hay secion , ocultamos el form , y mostramos el mensaje de iniciar secion ; 
                    pnlFormularioResena.Visible = false;
                    pnlMensajeLogin.Visible = true;
                }
            }
        }

        //ponemos  asi, para que sea llamado por java script ; 
        [System.Web.Services.WebMethod(EnableSession = true)]  
        public static string GuardarResenia (long idPunto, int puntaje, string comentario, long idUsuario)
        {
            try
            {
                if (idUsuario <= 0) return "Debes iniciar sesion.";

                Dominio.Reseña nueva = new Dominio.Reseña();
                nueva.IdPuntoReciclaje= idPunto;
                nueva.Puntaje = puntaje;
                nueva.Comentario = comentario;
                nueva.IdUsuario = idUsuario; 

                Negocio.ReseñaNegocio negocio = new Negocio.ReseñaNegocio();
                negocio.Agregar(nueva);

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

      



}
}
