using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio;

namespace Negocio
{
    public class ReseñaNegocio
    {
        public List<Reseña> ListarPorPunto(long idPunto)
        {
            List<Reseña> lista = new List<Reseña>();
            Accesodatos datos = new Accesodatos();
            try
            {
                
                datos.SetearConsulta(@"
                    SELECT R.ID_Reseña, R.ID_Usuario, R.ID_PuntoReciclaje, R.Reseña, R.Puntaje, R.FechaPublicacion, U.Nombre AS NombreUsuario
                    FROM Reseñas R
                    INNER JOIN Usuarios U ON R.ID_Usuario = U.ID_Usuario
                    WHERE R.ID_PuntoReciclaje = @idPunto
                    ORDER BY R.FechaPublicacion DESC"); 

                datos.agregarParametro("@idPunto", idPunto);

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reseña aux = new Reseña();

                    aux.IdReseña = (long)datos.Lector["ID_Reseña"];
                    aux.IdUsuario = (long)datos.Lector["ID_Usuario"];
                    aux.IdPuntoReciclaje     = (long)datos.Lector["ID_PuntoReciclaje"];


                    aux.Comentario = datos.Lector["Reseña"] != DBNull.Value ? (string)datos.Lector["Reseña"] : "";

                    aux.Puntaje = (byte)datos.Lector["Puntaje"]; 
                    aux.FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"];

                    aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception) { throw; }
            finally { datos.CerrarConexion(); }
        }

        public void Agregar(Reseña nueva)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Reseñas (ID_Usuario, ID_PuntoReciclaje, Reseña, Puntaje) VALUES (@idUsuario, @idPunto, @comentario, @puntaje)");
                datos.agregarParametro("@idUsuario", nueva.IdUsuario);
                datos.agregarParametro("@idPunto", nueva.IdPuntoReciclaje  );
                datos.agregarParametro("@comentario", nueva.Comentario);
                datos.agregarParametro("@puntaje", nueva.Puntaje);

                datos.ejecutarAccion();
            }
            catch (Exception) { throw; }
            finally { datos.CerrarConexion(); }
        }   




    }
}
