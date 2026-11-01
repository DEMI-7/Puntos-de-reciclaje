using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dominio;


namespace Negocio
{
    public class PuntoNegocio
    {
        public List<PuntoReciclaje> listar()
        {
            List<PuntoReciclaje> lista = new List<PuntoReciclaje>();

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta("SELECT ID_Punto, ID_UsuarioCreador, Direccion, Latitud, Longitud, Horario FROM Puntos_reciclaje");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    PuntoReciclaje aux = new PuntoReciclaje();

                    aux.IdPunto = (long)conexion.Lector["ID_Punto"];
                    aux.IdUsuarioCreador = (long)conexion.Lector["ID_UsuarioCreador"];
                    aux.Direccion = (string)conexion.Lector["Direccion"];
                    aux.Latitud = (decimal)conexion.Lector["Latitud"];
                    aux.Longitud = (decimal)conexion.Lector["Longitud"];
                    aux.Horario = (string)conexion.Lector["Horario"];

                    lista.Add(aux);
                }


                List<PuntoMaterial> listaRelaciones = listarRelacionpPuntosMateriales();

                foreach(PuntoReciclaje punto in lista)
                {
                    foreach(PuntoMaterial relacion in listaRelaciones)
                    {
                        if(punto.IdPunto == relacion.IdPunto)
                        {
                            punto.ListaMaterialesID.Add(relacion.IdMaterial);
                        }
                    }
                }


                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }

        }

        private List<PuntoMaterial> listarRelacionpPuntosMateriales()
        {
            List<PuntoMaterial> lista = new List<PuntoMaterial>();

            Accesodatos conexion = new Accesodatos();

            try
            {
                conexion.SetearConsulta("SELECT ID_Punto, ID_Material FROM Material_PuntoReciclaje");
                conexion.EjecutarLectura();

                while (conexion.Lector.Read())
                {
                    PuntoMaterial aux = new PuntoMaterial();

                    aux.IdPunto = (long)conexion.Lector["ID_Punto"];
                    aux.IdMaterial = (long)conexion.Lector["ID_Material"];


                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public List<PuntoReciclaje> ListarporEmpresa(long idEmpresa)
        {
            List<PuntoReciclaje> lista = new List<PuntoReciclaje>();
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
                    PuntoReciclaje xx = new PuntoReciclaje();
                    xx.IdPunto = (int)datos.Lector["ID_Punto"];
                    xx.IdUsuarioCreador  = (long)datos.Lector["ID_UsuarioCreador"];
                    xx.Direccion = (string)datos.Lector["Direccion"];
                    xx.Latitud = (decimal)datos.Lector["Latitud"];
                    xx.Longitud = (decimal)datos.Lector["Longitud"];
                    xx.Horario = (string)datos.Lector["Horario"];

                    //simulamos  materiales ; 
                    //xx.Materiales = "Ver detalle"; 

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