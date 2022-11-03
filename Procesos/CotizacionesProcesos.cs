using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoCRM.Procesos
{
    public class CotizacionesProcesos
    {

        //Funcion que lista la informacion de una cotizacion y la retorna
        public CotizacionesListado Listar(String id)
        {

            var cotizacion = new CotizacionesListado();



            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("obtenerCot", conexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {


                        cotizacion.NumeroCotizacion = dr["numeroCotizacion"].ToString();

                        cotizacion.NombreOportunidad = dr["nombreOportunidad"].ToString();

                        cotizacion.FechaCotizacion = (DateTime)dr["fechaCotizacion"];
                        cotizacion.FechaCierra = (DateTime)dr["fechaCierra"];

                        cotizacion.OrdenCompra = dr["ordenCompra"].ToString();
                        cotizacion.Descripcion = dr["descripcion"].ToString();
                        cotizacion.Factur = dr["factur"].ToString();
                        cotizacion.Zona = dr["zona"].ToString();
                        cotizacion.Sector = dr["sector"].ToString();

                        cotizacion.Moneda = dr["NombreMoneda"].ToString();
                        cotizacion.ContactoAsociado = dr["nombre"].ToString();
                        cotizacion.NombreCuenta = dr["cliente"].ToString();
                        cotizacion.Asesor = dr["asesor"].ToString();
                        cotizacion.Etapa = dr["etapa"].ToString();
                        cotizacion.Probabilidad = dr["proba"].ToString();
                        cotizacion.Tipo = dr["tipo"].ToString();
                        cotizacion.RazonDenegacion = dr["razon"].ToString();

                        cotizacion.ContraQuien = dr["rival"].ToString();





                    }
                }
               
            }

            return cotizacion;
        }
    }
}
