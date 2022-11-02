using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoCRM.Procesos
{
    public class contactosProcesos
    {
        public ContactoLista Listar(short id)
        {

            var contacto = new ContactoLista();



            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("obtenerContacto", conexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {


                        contacto.IdContacto = (short)dr["IdContacto"];

                        contacto.Nombre = dr["nombre"].ToString();

                        contacto.Motivo = dr["motivo"].ToString();
                        contacto.Telefono = dr["telefono"].ToString();
                        contacto.Correo = dr["correo"].ToString();
                        contacto.Direccion = dr["direccion"].ToString();
                        contacto.Descripcion = dr["descripcion"].ToString();
                        contacto.Cliente = dr["nombre_cuenta"].ToString();
                        contacto.Zona = dr["zona"].ToString();
                        contacto.Sector = dr["sector"].ToString();
                       contacto.Asesor = dr["asesor"].ToString();
                        contacto.TipoContacto = dr["tipo"].ToString();
                        contacto.Estado = dr["estado"].ToString();





                    }
                }
                return contacto;
            }


        }



    }
}








