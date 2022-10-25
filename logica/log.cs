using ProyectoCRM.Models;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace ProyectoCRM.logica
{
    public class log
    {

        public Usuario EncontrarUsuario (string usuario, string clave) 
        {
            string patron = "adjany";
            Usuario objeto = new Usuario();

            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true")) {

             //   string query = "select cedula, nombre, apellido1, apellido2, nombre_usuario, clave, rol, departamento from usuario where nombre_usuario = @usuario and clave = @pclave ";
                

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




      

    }
}
