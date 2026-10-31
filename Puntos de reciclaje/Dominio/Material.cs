using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puntos_de_reciclaje.Dominio
{
    public class Material
    {
        public long Idmaterial { get; set; }
        public string Nombre { get; set; }  
        public string Info { get; set; }

        public override string ToString()
        {
            return Nombre;
            //es para q el combox muestre el name automatico; 
        }
    }
}