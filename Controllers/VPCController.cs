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
    public class VPCController : Controller
    {
        private readonly CRMContext _context;

        public VPCController(CRMContext context)
        {
            _context = context;
        }

        // GET: VPC
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.ValorPresenteCotizaciones.Include(v => v.ContactoAsociadoNavigation).Include(v => v.NombreCuentaNavigation);
            return View(await cRMContext.ToListAsync());
        }

        // GET: VPC/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.ValorPresenteCotizaciones == null)
            {
                return NotFound();
            }

            var valorPresenteCotizacione = await _context.ValorPresenteCotizaciones
                .Include(v => v.ContactoAsociadoNavigation)
                .Include(v => v.NombreCuentaNavigation)
                .FirstOrDefaultAsync(m => m.IdCotizacion == id);
            if (valorPresenteCotizacione == null)
            {
                return NotFound();
            }

            return View(valorPresenteCotizacione);
        }

        // GET: VPC/Create
        public IActionResult Create()
        {
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "IdContacto");
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta");
            return View();
        }

        // POST: VPC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCotizacion,ContactoAsociado,NombreOportunidad,AnioCotizacion,NombreCuenta,TotalCotizacion,TotalValorPresente")] ValorPresenteCotizacione valorPresenteCotizacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valorPresenteCotizacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "IdContacto", valorPresenteCotizacione.ContactoAsociado);
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", valorPresenteCotizacione.NombreCuenta);
            return View(valorPresenteCotizacione);
        }

        // GET: VPC/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.ValorPresenteCotizaciones == null)
            {
                return NotFound();
            }

            var valorPresenteCotizacione = await _context.ValorPresenteCotizaciones.FindAsync(id);
            if (valorPresenteCotizacione == null)
            {
                return NotFound();
            }
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "IdContacto", valorPresenteCotizacione.ContactoAsociado);
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", valorPresenteCotizacione.NombreCuenta);
            return View(valorPresenteCotizacione);
        }

        // POST: VPC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdCotizacion,ContactoAsociado,NombreOportunidad,AnioCotizacion,NombreCuenta,TotalCotizacion,TotalValorPresente")] ValorPresenteCotizacione valorPresenteCotizacione)
        {
            if (id != valorPresenteCotizacione.IdCotizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valorPresenteCotizacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValorPresenteCotizacioneExists(valorPresenteCotizacione.IdCotizacion))
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
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "IdContacto", valorPresenteCotizacione.ContactoAsociado);
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", valorPresenteCotizacione.NombreCuenta);
            return View(valorPresenteCotizacione);
        }

        // GET: VPC/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.ValorPresenteCotizaciones == null)
            {
                return NotFound();
            }

            var valorPresenteCotizacione = await _context.ValorPresenteCotizaciones
                .Include(v => v.ContactoAsociadoNavigation)
                .Include(v => v.NombreCuentaNavigation)
                .FirstOrDefaultAsync(m => m.IdCotizacion == id);
            if (valorPresenteCotizacione == null)
            {
                return NotFound();
            }

            return View(valorPresenteCotizacione);
        }

        // POST: VPC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.ValorPresenteCotizaciones == null)
            {
                return Problem("Entity set 'CRMContext.ValorPresenteCotizaciones'  is null.");
            }
            var valorPresenteCotizacione = await _context.ValorPresenteCotizaciones.FindAsync(id);
            if (valorPresenteCotizacione != null)
            {
                _context.ValorPresenteCotizaciones.Remove(valorPresenteCotizacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValorPresenteCotizacioneExists(short id)
        {
          return _context.ValorPresenteCotizaciones.Any(e => e.IdCotizacion == id);
        }
    }
}
