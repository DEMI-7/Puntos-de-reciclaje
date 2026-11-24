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
                    WHERE R.ID_PuntoReciclaje = @idPunto AND R.Activo = 1 
                    ORDER BY R.FechaPublicacion DESC"); 

                datos.agregarParametro("@idPunto", idPunto);

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Reseña aux = new Reseña();

                    aux.IdResenia = (long)datos.Lector["ID_Reseña"];
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


        public List <Reseña> ListarTodas()
        {
            List<Reseña> lista = new List<Reseña>();
            Accesodatos datos = new Accesodatos();

            try
            {

                // hacemos el join whit users and puntos ; 
                datos.SetearConsulta(@"
                    SELECT R.ID_Reseña, R.ID_Usuario, R.ID_PuntoReciclaje, R.Reseña, R.Puntaje, R.FechaPublicacion,
                    U.Nombre AS NombreUsuario, P.Direccion AS DireccionPunto
                    FROM Reseñas R
                    INNER JOIN Usuarios U ON R.ID_Usuario = U.ID_Usuario
                    INNER JOIN Puntos_reciclaje P ON R.ID_PuntoReciclaje = P.ID_Punto
                    WHERE R.Activo = 1 
                    ORDER BY R.FechaPublicacion DESC");


                datos.EjecutarLectura();



                while (datos.Lector.Read())
                {
                    Reseña aux = new Reseña();

                    aux.IdResenia = (long)datos.Lector["ID_Reseña"];

                    aux.IdUsuario = (long)datos.Lector["ID_Usuario"];

                    aux.IdPuntoReciclaje = (long)datos.Lector["ID_PuntoReciclaje"];

                    aux.Comentario = datos.Lector["Reseña"] != DBNull.Value ? (string)datos.Lector["Reseña"] : "";

                    aux.Puntaje = (byte)datos.Lector["Puntaje"];

                    aux.FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"];

                    aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];

                    aux.DireccionPunto = (string)datos.Lector["DireccionPunto"];

                    lista.Add(aux);
                }
                return lista;

            }
            catch (Exception) 
            { throw; }
            finally 
            { datos.CerrarConexion(); }

        }


        //agregue elinar reseña para eliminar reseñas  , valga la redundancia xd 
         public void Eliminar(long idReseña, string motivo )
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta("UPDATE Reseñas SET Activo = 0," +
                    " MotivoEliminacion = @motivo WHERE ID_Reseña = @id");

                datos.agregarParametro("@motivo", motivo);

                datos.agregarParametro("@id", idReseña);
                datos.ejecutarAccion();
            }
            catch (Exception)
            { throw; }
            
        }


        public void Restaurar (long idReseña)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta("UPDATE Reseñas SET Activo = 1, MotivoEliminacion = NULL WHERE ID_Reseña = @id");
                datos.agregarParametro("@id", idReseña);
                datos.ejecutarAccion();
            }
            catch (Exception)
            { throw; }


        }



        public bool ExisteReseniaUser (long idUsuario, long idPunto)
        {
            Accesodatos datos = new Accesodatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Reseñas WHERE ID_Usuario = @idUsuario AND ID_PuntoReciclaje = @idPunto");
                datos.agregarParametro("@idUsuario", idUsuario);
                datos.agregarParametro("@idPunto", idPunto);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    int cant  = (int)datos.Lector[0]; // leemos el resultado de la consulta

                    return cant > 0; // si es mayor a 0 es porq ya existe ; 
                }
                return false; // si no hay resultados , no existe ;

            }
            catch (Exception) { throw; }
            finally { datos.CerrarConexion(); }




        }





    }
}
