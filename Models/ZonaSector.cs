using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class ZonaSector
    {
        public ZonaSector()
        {
            Clientes = new HashSet<Cliente>();
            Contactos = new HashSet<Contacto>();
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public string? Zona { get; set; }
        public string? Sector { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
