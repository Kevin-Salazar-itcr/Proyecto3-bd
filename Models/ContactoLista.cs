namespace ProyectoCRM.Models.ViewModels
{
    public class ContactoLista
    {
        public short IdContacto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Motivo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string Zona { get; set; }
        public string Sector { get; set; }
        public string Asesor { get; set; } = null!;
        public string TipoContacto { get; set; }
        public string Estado { get; set; }
    }
}
