using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoCRM.Models.ViewModels
{
    public class ProductoVM
    {

        public Producto ObjProducto { get; set; }

        public List <SelectListItem> ObjListaFamilia { get; set; }
    }
}
