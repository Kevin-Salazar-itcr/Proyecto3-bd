namespace ProyectoCRM.Models
{
    public class ProductoListado
    {


        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public short Activo { get; set; }
        public string CodigoFamilia { get; set; } = null!;

    }
}
