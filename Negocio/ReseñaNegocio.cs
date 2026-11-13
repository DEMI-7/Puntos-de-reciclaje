using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio;

namespace Negocio
{
    internal class ReseñaNegocio
    {
        public List<Reseña> listarPorPunto(long idPunto)
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

                    aux.idReseña = (long)datos.Lector["ID_Reseña"];
                    aux.idUsuario = (long)datos.Lector["ID_Usuario"];
                    aux.idPuntoReciclaje     = (long)datos.Lector["ID_PuntoReciclaje"];


                    aux.Comentario = datos.Lector["Reseña"] != DBNull.Value ? (string)datos.Lector["Reseña"] : "";

                    aux.Puntaje = (byte)datos.Lector["Puntaje"]; 
                    aux.fechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"];

                    aux.nombreUsuario = (string)datos.Lector["NombreUsuario"];

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
                datos.agregarParametro("@idUsuario", nueva.idUsuario);
                datos.agregarParametro("@idPunto", nueva.idPuntoReciclaje  );
                datos.agregarParametro("@comentario", nueva.Comentario);
                datos.agregarParametro("@puntaje", nueva.Puntaje);

                datos.ejecutarAccion();
            }
            catch (Exception) { throw; }
            finally { datos.CerrarConexion(); }
        }   




    }
}
