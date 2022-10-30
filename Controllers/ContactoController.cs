using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoCRM.Controllers
{
    public class ContactoController : Controller
    {
        private readonly CRMContext _context;

        public ContactoController(CRMContext context)
        {
            _context = context;
        }

        // GET: Contacto
        public async Task<IActionResult> Index()
        {

                    
            var viewModel = new ContactoIndex();
            viewModel.Contactos = await _context.Contactos

                  .Include(i => i.Tareas)
                  .Include(i => i.Actividads)
                  .Include(i => i.ZonaNavigation)
                  .Include(i => i.SectorNavigation)
                  .Include(i => i.AsesorNavigation)
                  .Include(i => i.TipoContactoNavigation)
                  .Include(i => i.EstadoNavigation)
                    
                        

                  .AsNoTracking()
                  .ToListAsync();

            return View(viewModel);
        }



           



        // GET: Contacto/Create/1
        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["Cliente"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta");
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Id");
            ViewData["Sector"] = new SelectList(_context.Sectors, "Id", "Id");
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Id");
            ViewData["Zona"] = new SelectList(_context.Zonas, "Id", "Id");
            return View();
        }

        // POST: Contacto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContacto,Nombre,Motivo,Telefono,Correo,Direccion,Descripcion,Cliente,Zona,Sector,Asesor,TipoContacto,Estado")] Contacto contacto)
        {

            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("agregarContacto", conexion);


                cmd.Parameters.AddWithValue("@idContacto", contacto.IdContacto);
                cmd.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("@motivo", contacto.Motivo);
                cmd.Parameters.AddWithValue("@telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("@correo", contacto.Correo);
                cmd.Parameters.AddWithValue("@direccion", contacto.Direccion);
                cmd.Parameters.AddWithValue("@descripcion", contacto.Descripcion);
                cmd.Parameters.AddWithValue("@cliente", contacto.Cliente);
                cmd.Parameters.AddWithValue("@zona", contacto.Zona);
                cmd.Parameters.AddWithValue("@sector", contacto.Sector);
                cmd.Parameters.AddWithValue("@asesor", contacto.Asesor);
                cmd.Parameters.AddWithValue("@tipoContacto", contacto.TipoContacto);
                cmd.Parameters.AddWithValue("@estado", contacto.Estado);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return RedirectToAction("index", "Home");


            }
        }

        // GET: Contacto/Edit/5
        public async Task<IActionResult> Edit(short? id)
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
            ViewData["Sector"] = new SelectList(_context.Sectors, "Id", "Id", contacto.Sector);
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Id", contacto.TipoContacto);
            ViewData["Zona"] = new SelectList(_context.Zonas, "Id", "Id", contacto.Zona);
            return View(contacto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdContacto,Nombre,Motivo,Telefono,Correo,Direccion,Descripcion,Cliente,Zona,Sector,Asesor,TipoContacto,Estado")] Contacto contacto)
        {
            if (id != contacto.IdContacto)
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
                    if (!ContactoExists(contacto.IdContacto))
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
            ViewData["Sector"] = new SelectList(_context.Sectors, "Id", "Id", contacto.Sector);
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Id", contacto.TipoContacto);
            ViewData["Zona"] = new SelectList(_context.Zonas, "Id", "Id", contacto.Zona);
            return View(contacto);
        }

        // GET: Contacto/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Contactos == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .Include(c => c.AsesorNavigation)
                .Include(c => c.ClienteNavigation)
                .Include(c => c.EstadoNavigation)
                .Include(c => c.SectorNavigation)
                .Include(c => c.TipoContactoNavigation)
                .Include(c => c.ZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdContacto == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
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

        private bool ContactoExists(short id)
        {
          return _context.Contactos.Any(e => e.IdContacto == id);
        }
    }
}
