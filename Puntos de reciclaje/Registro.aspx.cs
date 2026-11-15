using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Puntos_de_reciclaje
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnRegistrarme_Click(object sender, EventArgs e)
        {
            LblError.Visible = false;
            LblExito.Visible = false;

            string nombre = TxtNombre.Text.Trim();
            string email = TxtEmail.Text.Trim();
            string contraseña = TxtContraseña.Text;
            string repetirContraseña = TxtRepetirContraseña.Text;

            // validamos que no haya campos vacios
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(contraseña) || string.IsNullOrWhiteSpace(repetirContraseña))
            {
                LblError.Text = "Por favor, complete todos los campos";
                LblError.Visible = true;
                return;
            }
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                // validamos que el email no este registrado
                if (negocio.BuscarPorEmail(email) != null)
                {
                    LblError.Text = "Ya existe una cuenta registrada con ese correo";
                    LblError.Visible = true;
                    return;
                }

                // validamos que el email tenga arroba
                if (!email.Contains("@"))
                {
                    LblError.Text = "Por favor ingrese una casilla de mail valida.";
                    LblError.Visible = true;
                    return;
                }

                //validacion de contraseñas iguales
                if (contraseña != repetirContraseña)
                {
                    LblError.Text = "Las contraseñas no coinciden.";
                    LblError.Visible = true;
                    return;
                }

                // armamos el usuario nuevo
                Usuario nuevo = new Usuario();
                nuevo.Nombre = nombre;
                nuevo.Email = email;
                nuevo.Contraseña = contraseña;
                nuevo.TipoUsuario = DdlTipoCuenta.SelectedValue;

                //usuario basico se crea de una
                if (nuevo.TipoUsuario == "basico")
                {
                    nuevo.EstadoCuenta = "aprobado";
                    LblExito.Text = "Su cuenta fue creada exitosamente.";
                }
                else
                {
                    nuevo.EstadoCuenta = "pendiente";
                    LblExito.Text = "Su cuenta fue creada y mandada a revisión. Será verificada por un administrador.";
                }

                // guardar usuario en la bd
                negocio.Agregar(nuevo);

                //limpiamos los campos
                TxtNombre.Text = "";
                TxtEmail.Text = "";
                DdlTipoCuenta.SelectedIndex = 0;
                LblExito.Visible = true;

            }
            catch (Exception ex)
            {
                LblError.Text = "Error al crear la cuenta: " + ex.Message;
                LblError.Visible = true;
            }
        }
    }
}