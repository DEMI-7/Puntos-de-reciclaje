using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public  class Reseña
    {
        public long idReseña { get; set; }
        public long idUsuario { get; set; }

        public long idPuntoReciclaje { get; set; }
        public string Comentario { get; set; }
        public int Puntaje { get; set; }  // ponemos del 1 al 5 ; 

        public DateTime fechaPublicacion     { get; set; }


        public string nombreUsuario { get; set; } // para mostrar el nombre del usuario que hizo la reseña ;    
    }
}
