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
    public class ContactoesController : Controller
    {
        private readonly CRMContext _context;

        public ContactoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: Contactoes
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Contactos.Include(c => c.AsesorNavigation).Include(c => c.ClienteNavigation).Include(c => c.EstadoNavigation).Include(c => c.TipoContactoNavigation).Include(c => c.ZonaNavigation);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Contactoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Contactos == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .Include(c => c.AsesorNavigation)
                .Include(c => c.ClienteNavigation)
                .Include(c => c.EstadoNavigation)
                .Include(c => c.TipoContactoNavigation)
                .Include(c => c.ZonaNavigation)
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // GET: Contactoes/Create
        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["Cliente"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta");
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id");
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Id");
            ViewData["Zona"] = new SelectList(_context.ZonaSectors, "Id", "Id");
            return View();
        }

        // POST: Contactoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Motivo,Telefono,Correo,Direccion,Descripcion,Cliente,Zona,Asesor,TipoContacto,Estado")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", contacto.Asesor);
            ViewData["Cliente"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", contacto.Cliente);
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id", contacto.Estado);
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Id", contacto.TipoContacto);
            ViewData["Zona"] = new SelectList(_context.ZonaSectors, "Id", "Id", contacto.Zona);
            return View(contacto);
        }

        // GET: Contactoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Contactos == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", contacto.Asesor);
            ViewData["Cliente"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", contacto.Cliente);
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id", contacto.Estado);
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Id", contacto.TipoContacto);
            ViewData["Zona"] = new SelectList(_context.ZonaSectors, "Id", "Id", contacto.Zona);
            return View(contacto);
        }

        // POST: Contactoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Motivo,Telefono,Correo,Direccion,Descripcion,Cliente,Zona,Asesor,TipoContacto,Estado")] Contacto contacto)
        {
            if (id != contacto.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Nombre))
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
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", contacto.Asesor);
            ViewData["Cliente"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", contacto.Cliente);
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id", contacto.Estado);
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Id", contacto.TipoContacto);
            ViewData["Zona"] = new SelectList(_context.ZonaSectors, "Id", "Id", contacto.Zona);
            return View(contacto);
        }

        // GET: Contactoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Contactos == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .Include(c => c.AsesorNavigation)
                .Include(c => c.ClienteNavigation)
                .Include(c => c.EstadoNavigation)
                .Include(c => c.TipoContactoNavigation)
                .Include(c => c.ZonaNavigation)
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Contactos == null)
            {
                return Problem("Entity set 'CRMContext.Contactos'  is null.");
            }
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto != null)
            {
                _context.Contactos.Remove(contacto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(string id)
        {
          return _context.Contactos.Any(e => e.Nombre == id);
        }
    }
}
