using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Material
    {
        public long IdMaterial { get; set; }
        public string Nombre { get; set; }  
        public string Info { get; set; }

        public List<ImagenMaterial> ListaUrlImagenes { get; set; } = new List<ImagenMaterial>();

        public string PrimerImagen
        {
            get
            {
                if (ListaUrlImagenes.Count > 0)
                {
                    return ListaUrlImagenes[0].URL_Imagen;
                }
                else
                {
                    return null;
                }
            }
        }

        public override string ToString()
        {
            return Nombre;
            //es para q el combox muestre el name automatico; 
        }
    }
}