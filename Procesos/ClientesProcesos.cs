using ProyectoCRM.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoCRM.Procesos
{
    public class ClientesProcesos
    {

        //Funcion que lista todos los clientes de la base de datos

        public List<ClienteLista> Listar()
        {

            var oLista = new List<ClienteLista>();



            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("obtenerClientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new ClienteLista()
                        {
                            NombreCuenta = dr["nombre"].ToString(),

                            Celular = dr["celular"].ToString(),

                            Telefono = dr["telefono"].ToString(),
                            Correo = dr["correo"].ToString(),
                            Sitio = dr["sitio"].ToString(),
                            ContactoPrincipal = dr["contacto_principal"].ToString(),
                            Asesor = dr["asesor"].ToString(),
                            Idzona = dr["zona"].ToString(),
                            Idsector = dr["sector"].ToString(),
                            Idmoneda = dr["NombreMoneda"].ToString()





                        });

                    }
                }
            }

            return oLista;
        }



    }
}
