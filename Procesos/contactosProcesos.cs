using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoCRM.Procesos
{
    public class contactosProcesos
    {
     
        //FUncion que devuelve la informacion de un contacto
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

        //funcion que devuelve la informacion de todos los contactos
        public List<ContactoLista> ListarTodo()
        {

            var contacto = new ContactoLista();

            var oLista = new List<ContactoLista>();



            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("obtenerContactos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {


                    while (dr.Read())
                    {
                        oLista.Add(new ContactoLista()

                        {


                            IdContacto = (short)dr["IdContacto"],

                            Nombre = dr["nombre"].ToString(),

                            Motivo = dr["motivo"].ToString(),
                            Telefono = dr["telefono"].ToString(),
                            Correo = dr["correo"].ToString(),
                            Direccion = dr["direccion"].ToString(),
                            Descripcion = dr["descripcion"].ToString(),
                            Cliente = dr["nombre_cuenta"].ToString(),
                            Zona = dr["zona"].ToString(),
                            Sector = dr["sector"].ToString(),
                            Asesor = dr["asesor"].ToString(),
                            TipoContacto = dr["tipo"].ToString(),
                            Estado = dr["estado"].ToString()

                        });







                    }
                }
                return oLista;
            }


        }

        //La informacion que devuelve los contactos segun sus clientes
        public   List<ContactoLista>  ListarClient(String id)
        {

            var contacto = new ContactoLista();

            var oLista = new List<ContactoLista>();



            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("obtenerContactoCliente", conexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {


                    while (dr.Read())
                    {
                        oLista.Add(new ContactoLista()

                        {


                            IdContacto = (short)dr["IdContacto"],

                            Nombre = dr["nombre"].ToString(),

                            Motivo = dr["motivo"].ToString(),
                            Telefono = dr["telefono"].ToString(),
                            Correo = dr["correo"].ToString(),
                            Direccion = dr["direccion"].ToString(),
                            Descripcion = dr["descripcion"].ToString(),
                            Cliente = dr["nombre_cuenta"].ToString(),
                            Zona = dr["zona"].ToString(),
                            Sector = dr["sector"].ToString(),
                            Asesor = dr["asesor"].ToString(),
                            TipoContacto = dr["tipo"].ToString(),
                            Estado = dr["estado"].ToString()

                        });







                    }
                }
                return oLista;
            }


        }



    }
}








