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
        //carga primero el nombre de los dias
        private string[] nombresDias = { "lunes", "martes", "miércoles", "jueves", "viernes", "sábado", "domingo" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               CargarMateriales();
               CargarDesplegablesHorario();

                if (Request.QueryString["id"] != null)
                {
                    long idPunto = Convert.ToInt64(Request.QueryString["id"]);
                    Cargardatosparaeditar(idPunto);
                    btnGuardar.Text = "Actualizar Punto";
                    lblTitulo.Text = "Edicion de punto de reciclaje";

                }
            }

        }

        private void Cargardatosparaeditar(long idPunto)
        {
            PuntoNegocio negocio = new PuntoNegocio();
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                long idEmpresa = usuario != null ? usuario.ID_Usuario : 1;
                var lista = negocio.listar();
                var punto = lista.FirstOrDefault(p => p.IdPunto == idPunto && p.IdUsuarioCreador == idEmpresa);

                if (punto != null)
                {
                    txtDireccion.Text = punto.Direccion;
                    //txtHorario.Text = punto.Horario;
                    CargarHorario(punto.Horario);
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
                if (punto.ListaUrlImagenes != null && punto.ListaUrlImagenes.Count > 0)
                {
                    rptImagenes.DataSource = punto.ListaUrlImagenes;
                    rptImagenes.DataBind();
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

        private void CargarDesplegablesHorario()
        {
            // hora desde de 00:00 a 23:00
            for (int i = 0; i <= 23; i++)
            {
                ddlHoraDesde.Items.Add(i.ToString("00") + ":00");


            }
            // hora hasta de 01:00 a 23:00 pero con el 00:00 al final, para que el cierre pueda ser a las 12
            for (int i = 1; i <= 23; i++)
            {
                ddlHoraHasta.Items.Add(i.ToString("00") + ":00");
            }
            ddlHoraHasta.Items.Add("00:00");

            // horario por defecto
            ddlHoraDesde.SelectedValue = "09:00";
            ddlHoraHasta.SelectedValue = "18:00";
        }

        private string ArmarTextoDias(List<int> dias)
        {
            string texto;

            if (dias.Count == 7)
            {
                texto = "todos los días";
            }
            else if (dias.Count >= 3 && dias[dias.Count - 1] - dias[0] == dias.Count - 1)
            {
                // 3 o mas dias seguidos: "x a x"
                texto = nombresDias[dias[0]] + " a " + nombresDias[dias[dias.Count - 1]];
            }
            else
            {
                // dias sueltos: "lunes, miércoles, viernes"
                texto = string.Join(", ", dias.Select(d => nombresDias[d]));
            }

            // primera letra en mayuscula
            return char.ToUpper(texto[0]) + texto.Substring(1);
        }

        private void CargarHorario(string horario)
        {
            // sacamos los tildes por defecto de lunes a viernes
            foreach (ListItem dia in cblDias.Items)
            {
                dia.Selected = false;
            }

            try
            {
                // separacion de los dias y de las horas
                int posicion = horario.LastIndexOf(" de ");
                string textoDias = horario.Substring(0, posicion).ToLower();
                string[] horas = horario.Substring(posicion + 4).Split(new string[] { " a " }, StringSplitOptions.None);

                // horas
                ddlHoraDesde.SelectedValue = horas[0];
                ddlHoraHasta.SelectedValue = horas[1];

                // tildamos los dias segun el formato del texto
                if (textoDias == "todos los días")
                {
                    foreach (ListItem dia in cblDias.Items)
                    {
                        dia.Selected = true;
                    }
                }
                else if (textoDias.Contains(" a "))
                {
                    // x a x, tildamos desde el primero hasta el ultimo
                    string[] partes = textoDias.Split(new string[] { " a " }, StringSplitOptions.None);
                    int primerDia = Array.IndexOf(nombresDias, partes[0]);
                    int ultimoDia = Array.IndexOf(nombresDias, partes[1]);

                    for (int i = primerDia; i <= ultimoDia; i++)
                    {
                        cblDias.Items[i].Selected = true;
                    }
                }
                else
                {
                    // dias por separado
                    for (int i = 0; i < 7; i++)
                    {
                        if (textoDias.Contains(nombresDias[i]))
                        {
                            cblDias.Items[i].Selected = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            string latStr = Request.Form["latitudMapa"];
            string lngStr = Request.Form["longitudMapa"];

            if (string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(latStr))
            {
                lblError.Text = "Por favor, ingresa una direccion y selecciona una ubicacion en el mapa.";
                lblError.Visible = true;
                return;
            }

            //lista de dias tildados
            List<int> diasSeleccionados = new List<int>();
            foreach (ListItem dia in cblDias.Items)
            {
                if (dia.Selected)
                {
                    diasSeleccionados.Add(Convert.ToInt32(dia.Value));
                }
            }
            //validacion de un dia seleccionado al menos
            if (diasSeleccionados.Count == 0)
            {
                lblError.Text = "Por favor, seleccione al menos un día de atención.";
                lblError.Visible = true;
                return;
            }

            // validacion de horario de apertura sea antes del de cierre
            int horaDesde = ddlHoraDesde.SelectedIndex;
            int horaHasta = ddlHoraHasta.SelectedIndex + 1;
            if (horaHasta <= horaDesde)
            {
                lblError.Text = "La hora de cierre no puede ser antes que la hora de apertura.";
                lblError.Visible = true;
                return;
            }

            //texto de horario
            string horario = ArmarTextoDias(diasSeleccionados) + " de " + ddlHoraDesde.SelectedValue + " a " + ddlHoraHasta.SelectedValue;



            // TRY GENERAL
            try
            {
                // 1. Crear objeto
                PuntoReciclaje nuevoPunto = new PuntoReciclaje();
                Usuario usuario = (Usuario)Session["usuario"];
                nuevoPunto.IdUsuarioCreador = usuario != null ? usuario.ID_Usuario : 1;
                nuevoPunto.Horario = horario;
                nuevoPunto.Direccion = txtDireccion.Text;

                System.Globalization.CultureInfo cultura = System.Globalization.CultureInfo.InvariantCulture;
                nuevoPunto.Latitud = Math.Round(Convert.ToDecimal(latStr.Replace(',', '.'), cultura), 8);
                nuevoPunto.Longitud = Math.Round(Convert.ToDecimal(lngStr.Replace(',', '.'), cultura), 8);

                PuntoNegocio negocioPuntobich = new PuntoNegocio();
                long idPuntogenerado;

                // 2. Guardar o Modificar Punto
                if (Request.QueryString["id"] != null)
                {
                    nuevoPunto.IdPunto = Convert.ToInt64(Request.QueryString["id"]);
                    negocioPuntobich.Modificar(nuevoPunto);
                    negocioPuntobich.EliminarMaterialesDelPunto(nuevoPunto.IdPunto);
                    idPuntogenerado = nuevoPunto.IdPunto;
                }
                else
                {
                    idPuntogenerado = negocioPuntobich.Agregar(nuevoPunto);
                }

                // 3. Guardar Materiales
                foreach (ListItem xx in cblMateriales.Items)
                {
                    if (xx.Selected)
                    {
                        long idMaterial = Convert.ToInt64(xx.Value);
                        negocioPuntobich.AgregarMaterialalPunto(idPuntogenerado, idMaterial);
                    }
                }

                // 4. Guardar Imagen (TRY INTERNO)
                if (fuImagen.HasFile)
                {
                    try
                    {
                        string carpetaDestino = Server.MapPath("~/ImagenesPuntos/");

                        if (!System.IO.Directory.Exists(carpetaDestino))
                        {
                            System.IO.Directory.CreateDirectory(carpetaDestino);
                        }

                        string nombreArchivo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fuImagen.FileName);
                        string rutaFisica = carpetaDestino + nombreArchivo;
                        fuImagen.SaveAs(rutaFisica);

                        ImagenPuntoReciclaje nuevaFoto = new ImagenPuntoReciclaje();
                        nuevaFoto.IdPunto = idPuntogenerado;
                        nuevaFoto.UrlImagen = "ImagenesPuntos/" + nombreArchivo;

                        ImagenPuntoNegocio negocioImagen = new ImagenPuntoNegocio();
                        negocioImagen.Agregar(nuevaFoto);
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Error al guardar la foto: " + ex.Message;
                        lblError.Visible = true;
                        return; // Si falla la foto, cortamos todo
                    }
                }

                // 5. Redirigir (si todo salio bien)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='PanelEmpresa.aspx';", true);
            }
            catch (Exception ex)
            {
                // CATCH GENERAL (Errores de Base de Datos)
                lblError.Text = "Error #404 " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void rptImagenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarImg")
            {
                try
                {
                    long idImagen = Convert.ToInt64(e.CommandArgument);

                    ImagenPuntoNegocio negocioImg = new ImagenPuntoNegocio();

                    // 1. Borramos de la BD
                    negocioImg.Eliminar(idImagen);

                    // 2. Recargamos la página para que desaparezca de la vista
                    long idPunto = Convert.ToInt64(Request.QueryString["id"]);
                    Cargardatosparaeditar(idPunto);
                }
                catch (Exception ex)
                {
                    lblError.Text = "Error al borrar la imagen: " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }
    }
}
