using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public static class Seguridad
    {
        // panel admin: solo el admin
        public static bool PuedeVerPanelAdmin(object usuarioSesion)
        {
            if (usuarioSesion == null)
            {
                return false;
            }

            Usuario usuario = (Usuario)usuarioSesion;
            return usuario.TipoUsuario == "admin";
        }

        // panel empresa: empresa, gubernamental o admin
        public static bool PuedeVerPanelEmpresa(object usuarioSesion)
        {
            if (usuarioSesion == null)
            {
                return false;
            }

            Usuario usuario = (Usuario)usuarioSesion;
            return usuario.TipoUsuario == "empresa" || usuario.TipoUsuario == "gubernamental" || usuario.TipoUsuario == "admin";
        }
    }
}
