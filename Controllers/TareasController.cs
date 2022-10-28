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
    public class TareasController : Controller
    {
        private readonly CRMContext _context;

        public TareasController(CRMContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Tareas.Include(t => t.AsesorNavigation).Include(t => t.EstadoNavigation);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.AsesorNavigation)
                .Include(t => t.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id");
            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaFinalizacion,FechaCreacion,Informacion,Asesor,Estado")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", tarea.Asesor);
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id", tarea.Estado);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", tarea.Asesor);
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id", tarea.Estado);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,FechaFinalizacion,FechaCreacion,Informacion,Asesor,Estado")] Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.Id))
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
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", tarea.Asesor);
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id", tarea.Estado);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.AsesorNavigation)
                .Include(t => t.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Tareas == null)
            {
                return Problem("Entity set 'CRMContext.Tareas'  is null.");
            }
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(short id)
        {
          return _context.Tareas.Any(e => e.Id == id);
        }
    }
}
