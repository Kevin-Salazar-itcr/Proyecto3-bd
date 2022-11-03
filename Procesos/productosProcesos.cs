using System.Data;
using System.Data.SqlClient;
using ProyectoCRM.Models;


namespace ProyectoCRM.Procesos
{
    public class productosProcesos
    {

        //Funcion que lista la informacion de todos los productos existentes en la base de datos

        public List<Producto> Listar()
        {

            var oLista = new List<Producto>();

            

            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("obtenerProducto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new Producto()
                        {
                            Codigo = dr["codigo"].ToString()
,
                            Nombre = dr["nombre"].ToString(),

                            Descripcion = dr["descripcion"].ToString(),

                            Precio = (decimal)dr["precio"],

                            CodigoFamilia = dr["nombreF"].ToString(),

                            Activo = (short)dr["activo"]
                        }) ;

                    }
                }
            }

            return oLista;
        }


        //Funcion que recibe una producto y lo agrega a la base de datos
        public bool Guardar(Producto producto)
        {
            bool rpta;

            try
            {
                

                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("agregarProducto", conexion);


                    cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@activo", producto.Activo);
                    cmd.Parameters.AddWithValue("@codigo_familia", producto.CodigoFamilia);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
                rpta = true;


            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }



            return rpta;
        }

        //funcion para editar los campos de un producto en la base
        public bool Editar(Producto producto)
        {
            bool rpta;

            try
            {


                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {

                    SqlCommand cmd = new SqlCommand("editarProducto", conexion);


                    cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@activo", producto.Activo);
                    cmd.Parameters.AddWithValue("@familiaProducto", producto.CodigoFamilia);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

       


    }
}

    

