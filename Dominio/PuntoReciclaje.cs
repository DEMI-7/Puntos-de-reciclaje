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
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }   
        public string Horario { get; set; }

        
        public List<long> ListaMaterialesID { get; set; } = new List<long>();
        //prop para mostrar en la grilla; 

    }

    // Objeto que representa la relacion de material y punto de reciclaje
    public class PuntoMaterial
    {
        public long IdPunto { get; set; }
        public long IdMaterial { get; set; }
    }
}