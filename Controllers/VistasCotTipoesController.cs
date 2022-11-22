using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models3;

namespace ProyectoCRM.Controllers
{
    public class VistasCotTipoesController : Controller
    {
        private readonly CRMContext _context;

        public VistasCotTipoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistasCotTipoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistasCotTipos.ToListAsync());
        }
    }
}
