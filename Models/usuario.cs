namespace ProyectoCRM.Models
{
    public class usuario
    {
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string nombre_usuario { get; set; }
        public string clave { get; set; }
        public Rol rol { get; set; }
        public short  departamento { get; set; }
    }
}
