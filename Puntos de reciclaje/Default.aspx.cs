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

                // cargamos las reseñas para cada punto ; 
                ReseñaNegocio negocioReseña = new ReseñaNegocio();
                foreach (var punto in listaPuntos)
                {
                    punto.Resenias = negocioReseña.listarPorPunto(punto.IdPunto);
                }

                    // Falta Gestionar posible error con try catch
                    listaPuntos = negocio.listar();

                // Traducimos la lista a formato json asi el mapa puede dibujarlo con el javascript
                ListaPuntosJson = Newtonsoft.Json.JsonConvert.SerializeObject(listaPuntos);

                MaterialNegocio negocioMaterial = new MaterialNegocio();
                listaMateriales = negocioMaterial.listar();
                ListaMaterialesJson = Newtonsoft.Json.JsonConvert.SerializeObject(listaMateriales);
            }
        }
        //ponemos  asi, para que sea llamado por java script ; 
        [System.Web.Services.WebMethod]
        public static string GuardarResenia (long idPunto, int puntaje, string comentario)
        {
            try
            {
                // validaaamos qe el user esta logueado ; 
                if (System.Web.HttpContext.Current.Session["usuario"] == null)
                {
                    return "Debe iniciar sesión para dejar una reseña.";
                }
                // creamos la reseña ; 
                Dominio.Reseña nueva = new Dominio.Reseña();
                nueva.idPuntoReciclaje = idPunto;
                nueva.Puntaje = puntaje;
                nueva.Comentario = comentario;
                nueva.idUsuario = Convert.ToInt64(System.Web.HttpContext.Current.Session["usuario"]);

                //guardamos ; 

                Negocio.ReseñaNegocio negocio = new Negocio.ReseñaNegocio();
                negocio.Agregar(nueva);
                return "Reseña guardada correctamente."; 
            }
            catch (Exception ex)
            {
                return "Error al guardar la reseña: " + ex.Message;
            }




        }





    }
}