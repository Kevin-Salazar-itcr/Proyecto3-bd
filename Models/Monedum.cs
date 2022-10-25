using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Monedum
    {
        public Monedum()
        {
            Clientes = new HashSet<Cliente>();
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public string? NombreMoneda { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
