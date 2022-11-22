﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models3;

namespace ProyectoCRM.Controllers
{
    public class TopVentasVendedorsController : Controller
    {
        private readonly CRMContext _context;

        public TopVentasVendedorsController(CRMContext context)
        {
            _context = context;
        }

        // GET: TopVentasVendedors
        public async Task<IActionResult> Index()
        {
              return View(await _context.TopVentasVendedors.ToListAsync());
        }

    }
}