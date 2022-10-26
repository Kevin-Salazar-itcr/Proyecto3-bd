using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;

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
            var cRMContext = _context.Clientes.Include(c => c.AsesorNavigation).Include(c => c.IdmonedaNavigation).Include(c => c.IdzonaNavigation);
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
                .Include(c => c.AsesorNavigation)
                .Include(c => c.IdmonedaNavigation)
                .Include(c => c.IdzonaNavigation)
                .FirstOrDefaultAsync(m => m.NombreCuenta == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["Idmoneda"] = new SelectList(_context.Moneda, "Id", "Id");
            ViewData["Idzona"] = new SelectList(_context.ZonaSectors, "Id", "Id");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreCuenta,Celular,Telefono,Correo,Sitio,ContactoPrincipal,Asesor,Idzona,Idmoneda")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", cliente.Asesor);
            ViewData["Idmoneda"] = new SelectList(_context.Moneda, "Id", "Id", cliente.Idmoneda);
            ViewData["Idzona"] = new SelectList(_context.ZonaSectors, "Id", "Id", cliente.Idzona);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", cliente.Asesor);
            ViewData["Idmoneda"] = new SelectList(_context.Moneda, "Id", "Id", cliente.Idmoneda);
            ViewData["Idzona"] = new SelectList(_context.ZonaSectors, "Id", "Id", cliente.Idzona);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreCuenta,Celular,Telefono,Correo,Sitio,ContactoPrincipal,Asesor,Idzona,Idmoneda")] Cliente cliente)
        {
            if (id != cliente.NombreCuenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.NombreCuenta))
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
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", cliente.Asesor);
            ViewData["Idmoneda"] = new SelectList(_context.Moneda, "Id", "Id", cliente.Idmoneda);
            ViewData["Idzona"] = new SelectList(_context.ZonaSectors, "Id", "Id", cliente.Idzona);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.AsesorNavigation)
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
