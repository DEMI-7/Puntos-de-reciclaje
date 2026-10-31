using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puntos_de_reciclaje.Dominio
{
    public class Puntoreciclaje
    {
        public long Idpunto { get; set; }
        public long IdusuarioCreador { get; set; }
        public string Dirrecion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }   
        public string Horario { get; set; }

        
        public string Materiales { get; set; }
        //prop para mostrar en la grilla; 

    }
}