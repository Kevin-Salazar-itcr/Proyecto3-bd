using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCRM.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Contactos = new HashSet<Contacto>();
            Cotizaciones = new HashSet<Cotizacione>();
            Ejecucions = new HashSet<Ejecucion>();
        }

        [Display (Name = "Nombre de la cuenta")]
        [Required]
        [Key]
        public string NombreCuenta { get; set; } = null!;
        [Required]
        public string Celular { get; set; } = null!;
        [Required]
        public string Telefono { get; set; } = null!;
        [Required]
        public string Correo { get; set; } = null!;
        [Display(Name = "Sitio web")]
        [Required]
        public string Sitio { get; set; } = null!;
        [Required]
        [Display(Name = "Contacto principal")]
        public string ContactoPrincipal { get; set; } = null!;
        [Required]
        public string Asesor { get; set; } = null!;
        [Required]
        [Display(Name = "Zona")]
        public short Idzona { get; set; }
        [Required]
        [Display(Name = "Moneda")]
        public short Idmoneda { get; set; }

        [Display(Name = "Asesor")]
        public virtual Usuario AsesorNavigation { get; set; } = null!;

        [Display(Name = "Moneda")]
        public virtual Monedum IdmonedaNavigation { get; set; } = null!;
        [Display(Name = "Zona")]
        public virtual ZonaSector IdzonaNavigation { get; set; } = null!;



        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
    }
}
