using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;

namespace ProyectoCRM.Controllers
{

    [Authorize] 
    public class ProductoController : Controller
    {
        private readonly CRMContext _context;

        public ProductoController(CRMContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Productos.Include(p => p.ObjCodigoFamilia);
            return View(await cRMContext.ToListAsync());
        }




        [HttpGet]
        public IActionResult Create()

        {

            ProductoVM OproductoVM = new ProductoVM()
            {

                ObjProducto = new Producto(),
                ObjListaFamilia = _context.FamiliaProductos.Select(familia => new SelectListItem()
                {

                    Text = familia.Nombre,
                    Value = familia.Codigo.ToString()

                }).ToList()


            };

            return View(OproductoVM);

        }

        [HttpPost]
        public IActionResult Create(ProductoVM OproductoVM) {


            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("agregarProducto", conexion);


                cmd.Parameters.AddWithValue("@codigo", OproductoVM.ObjProducto.Codigo);
                cmd.Parameters.AddWithValue("@nombre", OproductoVM.ObjProducto.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", OproductoVM.ObjProducto.Descripcion);
                cmd.Parameters.AddWithValue("@precio", OproductoVM.ObjProducto.Precio);
                cmd.Parameters.AddWithValue("@activo", OproductoVM.ObjProducto.Activo);
                cmd.Parameters.AddWithValue("@codigo_familia", OproductoVM.ObjProducto.CodigoFamilia);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                

                

                return RedirectToAction("index", "Producto");





            }
        }





        //{
            //_context.Productos.Add(OproductoVM.ObjProducto);

            //_context.SaveChanges();



            //return RedirectToAction("index", "Home");

        //}













        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CodigoFamilia"] = new SelectList(_context.FamiliaProductos, "Codigo", "Codigo", producto.CodigoFamilia);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Nombre,Descripcion,Precio,Activo,CodigoFamilia")] Producto producto)
        {
            if (id != producto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoFamilia"] = new SelectList(_context.FamiliaProductos, "Codigo", "Codigo", producto.CodigoFamilia);
            return View(producto);
        }

        private bool ProductoExists(string id)
        {
          return _context.Productos.Any(e => e.Codigo == id);
        }
    }
}
