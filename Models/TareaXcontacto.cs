using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class TareaXcontacto
    {
        public short Contacto { get; set; }
        public short Tarea { get; set; }
        public string? Info { get; set; }

        public virtual Contacto ContactoNavigation { get; set; } = null!;
        public virtual Tarea TareaNavigation { get; set; } = null!;
    }
}
