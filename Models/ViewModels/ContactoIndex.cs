using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProyectoCRM.Models.ViewModels
{
    public class ContactoIndex
    {
        public IEnumerable<Contacto> Contactos { get; set; }
        public IEnumerable<Zona> Zonas { get; set; }
        public IEnumerable<Sector> Sectors { get; set; }
        public IEnumerable<TipoContacto> tipos { get; set; }
        public IEnumerable<Estado> Estados { get; set; }

    }
}
