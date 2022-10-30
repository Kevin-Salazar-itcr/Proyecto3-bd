using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;

namespace ProyectoCRM.Controllers
{
    public class ProductosCotizacionController : Controller
    {
        private readonly CRMContext _context;

        public ProductosCotizacionController(CRMContext context)
        {
            _context = context;
        }

        // GET: ProductosCotizacion
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.ProductosXcotizacions.Include(p => p.CodigoProductoNavigation).Include(p => p.NumeroCotizacionNavigation);
            return View(await cRMContext.ToListAsync());
        }


        // GET: ProductosCotizacion/Create
        public IActionResult Create()
        {
            ViewData["CodigoProducto"] = new SelectList(_context.Productos, "Codigo", "Nombre");
            ViewData["NumeroCotizacion"] = new SelectList(_context.Cotizaciones, "NumeroCotizacion", "NumeroCotizacion");
            return View();
        }

        // POST: ProductosCotizacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoProducto,NumeroCotizacion,Cantidad,PrecioNegociado")] ProductosXcotizacion productosXcotizacion)
        {
            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("agregarProductos", conexion);


                cmd.Parameters.AddWithValue("@codigo", productosXcotizacion.CodigoProducto);
                cmd.Parameters.AddWithValue("@numeroCot", productosXcotizacion.NumeroCotizacion);
                cmd.Parameters.AddWithValue("@cantidad", productosXcotizacion.Cantidad);
                cmd.Parameters.AddWithValue("@precioNegociado", productosXcotizacion.PrecioNegociado);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                return RedirectToAction("index", "Cotizaciones");


            }
        }


    }
}
