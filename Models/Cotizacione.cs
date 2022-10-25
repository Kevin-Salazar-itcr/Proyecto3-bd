using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Cotizacione
    {
        public Cotizacione()
        {
            Ejecucions = new HashSet<Ejecucion>();
            ProductosXcotizacions = new HashSet<ProductosXcotizacion>();
            ActividadCotizacions = new HashSet<Actividad>();
            TareaCotizacions = new HashSet<Tarea>();
        }

        public string NumeroCotizacion { get; set; } = null!;
        public string NombreOportunidad { get; set; } = null!;
        public DateTime FechaCotizacion { get; set; }
        public DateTime FechaCierra { get; set; }
        public string OrdenDeCompra { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public short Zona { get; set; }
        public short Moneda { get; set; }
        public string ContactoAsociado { get; set; } = null!;
        public string Asesor { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public short Etapa { get; set; }
        public short Probabilidad { get; set; }
        public short Tipo { get; set; }
        public short Inflacion { get; set; }

        public virtual Usuario AsesorNavigation { get; set; } = null!;
        public virtual Contacto ContactoAsociadoNavigation { get; set; } = null!;
        public virtual Etapa EtapaNavigation { get; set; } = null!;
        public virtual Inflacion InflacionNavigation { get; set; } = null!;
        public virtual Monedum MonedaNavigation { get; set; } = null!;
        public virtual Cliente NombreCuentaNavigation { get; set; } = null!;
        public virtual Probabilidad ProbabilidadNavigation { get; set; } = null!;
        public virtual TipoCotizacion TipoNavigation { get; set; } = null!;
        public virtual ZonaSector ZonaNavigation { get; set; } = null!;
        public virtual CotizacionDenegadum? CotizacionDenegadum { get; set; }
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
        public virtual ICollection<ProductosXcotizacion> ProductosXcotizacions { get; set; }

        public virtual ICollection<Actividad> ActividadCotizacions { get; set; }
        public virtual ICollection<Tarea> TareaCotizacions { get; set; }
    }
}
