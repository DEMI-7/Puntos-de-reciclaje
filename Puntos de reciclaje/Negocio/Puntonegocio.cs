using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Puntos_de_reciclaje.Datos;
using Puntos_de_reciclaje.Dominio;


namespace Puntos_de_reciclaje.Negocio
{
    public class Puntonegocio
    {
        public List<Puntoreciclaje> ListarporEmpresa(long    idEmpresa)
        {
            List<Puntoreciclaje> lista = new List<Puntoreciclaje>();
            Accesodatos datos = new Accesodatos();
            try
            {
                //traemos los puntos de una emp especifica ; 
                //materiales : simulamos en un string de momento ; 

                datos.SetearConsulta("SELECT ID_Punto, ID_UsuarioCreador, Direccion, Latitud, Longitud," +
                    " Horario FROM Puntos_reciclaje WHERE ID_UsuarioCreador = @idEmpresa"); 


                datos.agregarParametro("@idEmpresa", idEmpresa);

                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {
                    Puntoreciclaje xx = new Puntoreciclaje();
                    xx.Idpunto = (int)datos.Lector["ID_Punto"];
                    xx.IdusuarioCreador  = (long)datos.Lector["ID_UsuarioCreador"];
                    xx.Dirrecion = (string)datos.Lector["Direccion"];
                    xx.Latitud = (decimal)datos.Lector["Latitud"];
                    xx.Longitud = (decimal)datos.Lector["Longitud"];
                    xx.Horario = (string)datos.Lector["Horario"];

                    //simulamos  materiales ; 
                    xx.Materiales = "Ver detalle"; 

                    lista.Add(xx);
                }
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}