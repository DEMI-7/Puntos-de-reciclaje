using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public  class Reseña
    {
        public long IdResenia { get; set; }
        public long IdUsuario { get; set; }

        public long IdPuntoReciclaje { get; set; }
        public string Comentario { get; set; }
        public int Puntaje { get; set; }  // ponemos del 1 al 5 ; 

        public DateTime FechaPublicacion     { get; set; }


        public string NombreUsuario { get; set; } // para mostrar el nombre del usuario que hizo la reseña ;    
        public string DireccionPunto { get; set; } // para mostrar la dirección del punto de reciclaje ; 


        // NEW  ; 
        public bool Activo { get; set; } // para saber si la reseña esta activa o no ;
        public string MotivoEliminacion { get; set; } // para saber el motivo de la eliminación de la reseña ;
    }
}
