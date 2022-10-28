using ProyectoCRM.Models;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
using System.Security.Policy;

namespace ProyectoCRM.logica
{
    public class log
    {

        public Usuario EncontrarUsuario (string usuario, string clave) 
        {
            string patron = "adjany";
            Usuario objeto = new Usuario();

            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true")) {

                

                SqlCommand cmd = new SqlCommand ("validarUsuario", conexion);

                cmd.Parameters.AddWithValue("@usario", usuario);
                cmd.Parameters.AddWithValue("@clave", clave);
                cmd.Parameters.AddWithValue("@patron", patron);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read()) {

                        objeto = new Usuario()
                        {


                            Cedula = dr["cedula"].ToString(),
                            Nombre = dr["nombre"].ToString(),
                            Apellido1 = dr["apellido1"].ToString(),
                            Apellido2 = dr["apellido2"].ToString(),
                            NombreUsuario = dr["nombre_usuario"].ToString(),

                            Clave = dr["clave"].ToString(),

                            Rol = (short)dr["rol"],

                            Departamento = (short)dr["departamento"]

                        };
                    
                    }

                }

                return objeto;
            }
        
        
        }





        public Contacto EncontrarContacto(short contacto)
        {
            
            Contacto contact = new Contacto();

            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {

                SqlCommand cmd = new SqlCommand("validarContacto", conexion);

                cmd.Parameters.AddWithValue("@contacto", contacto);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        contact = new Contacto()
                        {

                            Zona = (short)dr["zona"],
                            Asesor = dr["asesor"].ToString(),
                            Cliente = dr["cliente"].ToString(),
                            Sector = (short)dr["sector"],

                        };

                    }


                    
                }

                return contact;


            }


        }



    }
}
