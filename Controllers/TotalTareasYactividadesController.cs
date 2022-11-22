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
    public class TotalTareasYactividadesController : Controller
    {
        private readonly CRMContext _context;

        public TotalTareasYactividadesController(CRMContext context)
        {
            _context = context;
        }

        // GET: TotalTareasYactividades
        public async Task<IActionResult> Index()
        {
              return View(await _context.TotalTareasYactividades.ToListAsync());
        }

    }
}
