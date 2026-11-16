using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class RegistroAccion
    {

        public long ID_Movimiento { get; set; }
        public Usuario Admin { get; set; } = new Usuario();
        public Usuario UsuarioAfectado { get; set; } = new Usuario();
        public string EstadoAnterior { get; set; } = string.Empty;
        public string EstadoNuevo { get; set; } = string.Empty;
        // Detalle hasta 300 caracteres varchar
        public string Detalle {  get; set; } = string.Empty;
        public DateTime HoraMovimiento { get; set; }

    }
}
