using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Inflacion
    {
        public Inflacion()
        {
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public double Porcentaje { get; set; }

        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
