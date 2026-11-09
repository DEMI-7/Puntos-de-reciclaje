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
    public partial class Aprender : System.Web.UI.Page
    {
        List<Material> listaMateriales;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MaterialesSesion"] != null)
            {
                listaMateriales = (List<Material>)Session["MaterialesSesion"];
            }
            else
            {
                // Si no está, la buscamos en la DB y la guardamos en la Sesión
                MaterialNegocio negocio = new MaterialNegocio();
                listaMateriales = negocio.ListarConImagenes();
                Session["MaterialesSesion"] = listaMateriales;
            }

            if (!IsPostBack)
            {
                Repetidor.DataSource = listaMateriales;
                Repetidor.DataBind();

                if (listaMateriales != null && listaMateriales.Count > 0 && Session["IdMaterialSeleccionado"] == null)
                {
                    Material primerMaterial = listaMateriales[0];
                    Session["IdMaterialSeleccionado"] = primerMaterial.IdMaterial;
                }
            }

            // Si hay algo guardado en la sesión al recargar, mostramos su info
            if (Session["IdMaterialSeleccionado"] != null)
            {
                long idMaterial = Convert.ToInt64(Session["IdMaterialSeleccionado"]);

                Material mat = listaMateriales.FirstOrDefault(m => m.IdMaterial == idMaterial);

                if (mat != null)
                {
                    txtTitulo.Text = mat.Nombre;
                    txtInfo.Text = mat.Info;
                    RepetidorImgMateriales.DataSource = mat.ListaUrlImagenes;
                    RepetidorImgMateriales.DataBind();
                }
            }
        }

        protected void ImgLogo_Command(object sender, CommandEventArgs e)
        {
            long idMaterial = Convert.ToInt64(e.CommandArgument);
            Session["IdMaterialSeleccionado"] = idMaterial;
            // redirije para que no salga el cartel de cancelar formulario al recargar la pagina
            Response.Redirect(Request.RawUrl);
        }
    }
}