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
    public class ActividadController : Controller
    {
        private readonly CRMContext _context;

        public ActividadController(CRMContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Actividads.Include(a => a.AsesorNavigation);
            return View(await cRMContext.ToListAsync());
        }


        public IActionResult Create(short? id)
        {

            Globales.contacto = (short)id;
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaInicio,FechaFin,Asesor")] Actividad actividad)
        {
            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("agregarActividad", conexion);


                cmd.Parameters.AddWithValue("@idContacto", Globales.contacto);
                cmd.Parameters.AddWithValue("@id", actividad.Id);
                cmd.Parameters.AddWithValue("@descripcion", actividad.Descripcion);
                cmd.Parameters.AddWithValue("@fechaIni", actividad.FechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", actividad.FechaFin);
                cmd.Parameters.AddWithValue("@asesor", actividad.Asesor);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                return RedirectToAction("index", "Contacto");


            }
        }


        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Actividads == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividads.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", actividad.Asesor);
            return View(actividad);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Descripcion,FechaInicio,FechaFin,Asesor")] Actividad actividad)
        {
            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("editarActividad", conexion);


                cmd.Parameters.AddWithValue("@id", actividad.Id);
                cmd.Parameters.AddWithValue("@fechaFin", actividad.FechaFin);
                cmd.Parameters.AddWithValue("@asesor", actividad.Asesor);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                return RedirectToAction("index", "Actividad");


            }
        }



    }
}
