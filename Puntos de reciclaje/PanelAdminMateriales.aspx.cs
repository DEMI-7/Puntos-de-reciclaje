using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Puntos_de_reciclaje
{
    public partial class PanelAdminMateriales : System.Web.UI.Page
    {
        List<Material> listaMateriales;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaMateriales();
            }
            LblMensajeSubidaImagen.Style["visibility"] = "hidden";
            BtnSubirFoto.Style["visibility"] = "hidden";
            FuImagenesMateriales.Style["visibility"] = "hidden";
            tituloImagenesMat.Style["visibility"] = "hidden";
        }

        private void CargarGrillaMateriales()
        {
            MaterialNegocio negocio = new MaterialNegocio();
            listaMateriales = negocio.ListarConImagenes();

            GrillaMateriales.DataSource = listaMateriales;
            GrillaMateriales.DataBind();

            LblCantidadMateriales.Text = listaMateriales.Count.ToString();
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
                Material material = negocio.ListarConImagenes().FirstOrDefault(m => m.IdMaterial == idMaterial);

                if (material != null)
                {
                    HfIdMaterial.Value = material.IdMaterial.ToString();
                    TxtNombre.Text = material.Nombre;
                    TxtInfo.Text = material.Info;
                    LblTituloFormulario.Text = "Modificar material";
                    ImgLogo.ImageUrl = material.UrlLogo;
                    HfUrlLogo.Value = material.UrlLogo;

                    rptImagenes.DataSource = material.ListaUrlImagenes;
                    rptImagenes.DataBind();

                    BtnSubirFoto.Style["visibility"] = "visible";
                    FuImagenesMateriales.Style["visibility"] = "visible";
                    tituloImagenesMat.Style["visibility"] = "visible";


                }
            }
            else if (e.CommandName == "Eliminar")
            {
                long idMaterial = Convert.ToInt64(e.CommandArgument);
                negocio.Eliminar(idMaterial);

                // si justo estabamos editando este material, limpiamos el formulario
                if (HfIdMaterial.Value == idMaterial.ToString())
                {
                    LimpiarFormulario();
                }

                CargarGrillaMateriales();
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
                material.UrlLogo = HfUrlLogo.Value;

                MaterialNegocio negocio = new MaterialNegocio();

                ActualizarSubirLogo(material);

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
            tituloImagenesMat.Style["visibility"] = "hidden";
            LblMensajeSubidaImagen.Style["visibility"] = "hidden";
            FuImagenesMateriales.Style["visibility"] = "hidden";
            BtnSubirFoto.Style["visibility"] = "hidden";
            rptImagenes.DataSource = null;
            rptImagenes.DataBind();
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdministrador.aspx", false);
        }


        private void ActualizarSubirLogo(Material mat)
        {
            // si el upload file esta vacio directamente no ejecuta ninguna logica referida a los archivos de imagenes
            if (!SubirImagenLogo.HasFile)
            {
                return;
            }

            try
            {
                string carpetaDestino = Server.MapPath("~/ImagenesMateriales/");

                if (!System.IO.Directory.Exists(carpetaDestino))
                {
                    System.IO.Directory.CreateDirectory(carpetaDestino);
                }

                // primero me fijo si la imagen ya existia y la borro
                if (!string.IsNullOrEmpty(mat.UrlLogo))
                {
                    string rutaAnteriorFisica = Server.MapPath("~/" + mat.UrlLogo.TrimStart('~', '/'));

                    if (System.IO.File.Exists(rutaAnteriorFisica))
                    {
                        System.IO.File.Delete(rutaAnteriorFisica);
                    }
                }

                // despues la guardo si es una imagen nueva
                string nombreArchivo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(SubirImagenLogo.FileName);
                string rutaFisica = carpetaDestino + nombreArchivo;

                SubirImagenLogo.SaveAs(rutaFisica);

                mat.UrlLogo = "ImagenesMateriales/" + nombreArchivo;

            }
            catch (Exception)
            {
                LblError.Text = "Error al subir la imagen";
                LblError.Visible = true;
                return;
            }
        }

        protected void BtnSubirFoto_Click(object sender, EventArgs e)
        {
            // si el upload file esta vacio directamente no ejecuta ninguna logica referida a los archivos de imagenes
            if (!FuImagenesMateriales.HasFile || string.IsNullOrEmpty(HfIdMaterial.Value))
            {
                LblMensajeSubidaImagen.Text = "Primero seleccione un archivo";
                LblMensajeSubidaImagen.Style["visibility"] = "visible";
                LblMensajeSubidaImagen.ForeColor = Color.Red;
                return;
            }

            try
            {
                ImagenMaterial imagen = new ImagenMaterial();
                imagen.ID_Material = Convert.ToInt64(HfIdMaterial.Value);
                imagen.URL_Imagen = "";

                string carpetaDestino = Server.MapPath("~/ImagenesMateriales/");

                if (!System.IO.Directory.Exists(carpetaDestino))
                {
                    System.IO.Directory.CreateDirectory(carpetaDestino);
                }

                
                string nombreArchivo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(FuImagenesMateriales.FileName);
                string rutaFisica = carpetaDestino + nombreArchivo;

                FuImagenesMateriales.SaveAs(rutaFisica);

                imagen.URL_Imagen = "ImagenesMateriales/" + nombreArchivo;

                ImagenMaterialNegocio negocio = new ImagenMaterialNegocio();
                negocio.SubirImagenPaginaMaterial(imagen);
                LblMensajeSubidaImagen.Text = "Imagen subida correctamente";
                LblMensajeSubidaImagen.Style["visibility"] = "visible";
                LblMensajeSubidaImagen.ForeColor = Color.Green;
                BtnSubirFoto.Style["visibility"] = "visible";
                tituloImagenesMat.Style["visibility"] = "visible";

                CargarGrillaMateriales();
                Material material = listaMateriales.FirstOrDefault(m => m.IdMaterial == Convert.ToInt64(HfIdMaterial.Value));
                rptImagenes.DataSource = material.ListaUrlImagenes;
                rptImagenes.DataBind();

            }
            catch (Exception)
            {
                LblError.Text = "Error al subir la imagen";
                LblError.Visible = true;
                return;
            }
            finally
            {
                FuImagenesMateriales.Attributes.Clear();
            }
        }

        protected void rptImagenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Verificamos si el comando ejecutado es el de eliminar
            if (e.CommandName == "EliminarImg")
            {
                ImagenMaterialNegocio negocio = new ImagenMaterialNegocio();
                // Capturar el id en el comand argument
                int idFoto = Convert.ToInt32(e.CommandArgument);
                // borrado logico
                negocio.BorradoLogicoImagenMaterial(idFoto);

                // recargar repetear
                CargarGrillaMateriales();
                Material material = listaMateriales.FirstOrDefault(m => m.IdMaterial == Convert.ToInt64(HfIdMaterial.Value));
                rptImagenes.DataSource = material.ListaUrlImagenes;
                rptImagenes.DataBind();

                LblMensajeSubidaImagen.Text = "Imagen eliminada correctamente";
                LblMensajeSubidaImagen.Style["visibility"] = "visible";
                LblMensajeSubidaImagen.ForeColor = Color.DarkRed;
                BtnSubirFoto.Style["visibility"] = "visible";
                FuImagenesMateriales.Style["visibility"] = "visible";
                tituloImagenesMat.Style["visibility"] = "visible";
            }
        }
    }
}