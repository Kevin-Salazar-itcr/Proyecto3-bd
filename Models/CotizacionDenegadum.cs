using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class CotizacionDenegadum
    {
        public string NumeroCotizacion { get; set; } = null!;
        public string? Razon { get; set; }

        public virtual Cotizacione NumeroCotizacionNavigation { get; set; } = null!;
    }
}
