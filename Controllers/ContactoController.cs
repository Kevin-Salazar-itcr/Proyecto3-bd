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
            var cRMContext = _context.Contactos.Include(c => c.AsesorNavigation).Include(c => c.ClienteNavigation).Include(c => c.EstadoNavigation).Include(c => c.TipoContactoNavigation).Include(c => c.ZonaNavigation);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Contacto/Details/5
        public async Task<IActionResult> Details(short? id)
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
                .FirstOrDefaultAsync(m => m.IdContacto == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }


        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["Cliente"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta");
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Estado1");
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Tipo");
            ViewData["Zona"] = new SelectList(_context.ZonaSectors, "Id", "Zona");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContacto,Nombre,Motivo,Telefono,Correo,Direccion,Descripcion,Cliente,Zona,Asesor,TipoContacto,Estado")] Contacto contacto)
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
                cmd.Parameters.AddWithValue("@asesor", contacto.Asesor);
                cmd.Parameters.AddWithValue("@tipoContacto", contacto.TipoContacto);
                cmd.Parameters.AddWithValue("@estado", contacto.Estado);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return RedirectToAction("index", "Home");


            }

        }
    }
}


