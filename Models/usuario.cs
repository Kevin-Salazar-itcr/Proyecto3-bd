using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Clientes = new HashSet<Cliente>();
            Contactos = new HashSet<Contacto>();
            Cotizaciones = new HashSet<Cotizacione>();
            Ejecucions = new HashSet<Ejecucion>();
        }

        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido1 { get; set; } = null!;
        public string Apellido2 { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public short Rol { get; set; }
        public short Departamento { get; set; }

        public virtual Departamento DepartamentoNavigation { get; set; } = null!;
        public virtual Rol RolNavigation { get; set; } = null!;
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
    }
}
