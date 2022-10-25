using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCRM.Models
{
    public partial class Producto
    {
        public Producto()
        {
            ProductosXcotizacions = new HashSet<ProductosXcotizacion>();
        }

        [Key]
        [Required]
        public string Codigo { get; set; } = null!;
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public short Activo { get; set; }
        [Required]
        public string CodigoFamilia { get; set; } = null!;


        
        [Display(Name =  "Familia de producto")]
        [Required]
        public virtual FamiliaProducto ObjCodigoFamilia { get; set; } = null!;
        public virtual ICollection<ProductosXcotizacion> ProductosXcotizacions { get; set; }
    }
}
