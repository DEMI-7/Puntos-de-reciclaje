using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class PuntoReciclaje
    {
        public long IdPunto { get; set; }
        public long IdUsuarioCreador { get; set; }
        public string Dirrecion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }   
        public string Horario { get; set; }

        
        public string Materiales { get; set; }
        //prop para mostrar en la grilla; 

    }
}