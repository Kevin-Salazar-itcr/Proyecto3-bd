using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models3;

/**
 * Controlador de la consulta de la diferencia en dias de cotizaciones
 */

namespace ProyectoCRM.Controllers
{
    public class DiferEnciaDiasCotsController : Controller
    {
        private readonly CRMContext _context;

        public DiferEnciaDiasCotsController(CRMContext context)
        {
            _context = context;
        }

        // GET: DiferEnciaDiasCots
        public async Task<IActionResult> Index()
        {
              return View(await _context.DiferEnciaDiasCots.ToListAsync());
        }

    }
}
