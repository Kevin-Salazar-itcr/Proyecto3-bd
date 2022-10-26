using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;

namespace ProyectoCRM.Controllers
{
    public class ClientesController : Controller
    {
        private readonly CRMContext _context;

        public ClientesController(CRMContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Clientes.Include(c => c.IdmonedaNavigation).Include(c => c.IdzonaNavigation);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdmonedaNavigation)
                .Include(c => c.IdzonaNavigation)
                .FirstOrDefaultAsync(m => m.NombreCuenta == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }







        [HttpGet]
        public IActionResult Create()

        {

          
            

            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["Idmoneda"] = new SelectList(_context.Moneda, "Id", "NombreMoneda");
            ViewData["Idzona"] = new SelectList(_context.ZonaSectors, "Id", "Zona");
            return View();




        }

        [HttpPost]
        public IActionResult Create([Bind("NombreCuenta,Celular,Telefono,Correo,Sitio,ContactoPrincipal,Asesor,Idzona,Idmoneda")] Cliente cliente)
        {


            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("agregarCliente", conexion);


                cmd.Parameters.AddWithValue("@nombre_cuenta", cliente.NombreCuenta);
                cmd.Parameters.AddWithValue("@celular", cliente.Celular);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@sitio", cliente.Sitio);
                cmd.Parameters.AddWithValue("@contactoP", cliente.ContactoPrincipal);
                cmd.Parameters.AddWithValue("@asesor", User.Identity.Name.ToString());
                cmd.Parameters.AddWithValue("@zona", cliente.Idzona);
                cmd.Parameters.AddWithValue("@moneda", cliente.Idmoneda);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return RedirectToAction("index", "Home");
            }
        }



        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdmonedaNavigation)
                .Include(c => c.IdzonaNavigation)
                .FirstOrDefaultAsync(m => m.NombreCuenta == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'CRMContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(string id)
        {
          return _context.Clientes.Any(e => e.NombreCuenta == id);
        }
    }
}
