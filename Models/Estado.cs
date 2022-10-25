using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Contactos = new HashSet<Contacto>();
        }

        public short Id { get; set; }
        public string Estado1 { get; set; } = null!;

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
