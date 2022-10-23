using ProyectoCRM.Models;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace ProyectoCRM.logica
{
    public class log
    {

        public usuario EncontrarUsuario (string usuario, string clave) 
        {
            usuario objeto = new usuario();

            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true")) {

                string query = "select cedula, nombre, apellido1, apellido2, nombre_usuario, clave, rol, departamento from usuario where nombre_usuario = @usuario and clave = @pclave ";
                
                SqlCommand cmd = new SqlCommand (query, conexion);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@pclave", clave);
                cmd.CommandType = System.Data.CommandType.Text;

                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read()) {

                        objeto = new usuario()
                        {


                            cedula = dr["cedula"].ToString(),
                            nombre = dr["nombre"].ToString(),
                            apellido1 = dr["apellido1"].ToString(),
                            apellido2 = dr["apellido2"].ToString(),
                            nombre_usuario = dr["nombre_usuario"].ToString(),

                            clave = dr["clave"].ToString(),

                            rol = (Rol)(short)dr["rol"],

                            departamento = (short)dr["departamento"]

                        };
                    
                    }

                }

                return objeto;
            }
        
        
        }




      

    }
}
