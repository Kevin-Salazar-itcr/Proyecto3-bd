namespace ProyectoCRM.Models
{
    public class CotizacionesListado
    {
        public string NumeroCotizacion { get; set; } = null!;
        public string NombreOportunidad { get; set; } = null!;
        public DateTime FechaCotizacion { get; set; }
        public DateTime FechaCierra { get; set; }
        public string OrdenCompra { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Factur { get; set; } = null!;
        public string Zona { get; set; }
        public string Sector { get; set; }
        public string Moneda { get; set; }
        public string ContactoAsociado { get; set; }
        public string Asesor { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public string Etapa { get; set; }
        public string Probabilidad { get; set; }
        public string Tipo { get; set; }
        public string? RazonDenegacion { get; set; }
        public string? ContraQuien { get; set; }






    }
}
