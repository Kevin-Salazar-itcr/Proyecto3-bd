using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class ActividadesXcontacto
    {
        public short Contacto { get; set; }
        public short Actividad { get; set; }
        public string? Info { get; set; }

        public virtual Actividad ActividadNavigation { get; set; } = null!;
        public virtual Contacto ContactoNavigation { get; set; } = null!;
    }
}
