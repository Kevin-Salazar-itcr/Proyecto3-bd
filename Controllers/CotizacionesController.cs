using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.logica;

namespace ProyectoCRM.Controllers
{
    public class CotizacionesController : Controller
    {
        private readonly CRMContext _context;

        public CotizacionesController(CRMContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Cotizaciones.Include(c => c.AsesorNavigation).Include(c => c.ContactoAsociadoNavigation).Include(c => c.ContraQuienNavigation).Include(c => c.EtapaNavigation).Include(c => c.MonedaNavigation).Include(c => c.NombreCuentaNavigation).Include(c => c.ProbabilidadNavigation).Include(c => c.RazonDenegacionNavigation).Include(c => c.SectorNavigation).Include(c => c.TipoNavigation).Include(c => c.ZonaNavigation);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Cotizaciones/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacione = await _context.Cotizaciones
                .Include(c => c.AsesorNavigation)
                .Include(c => c.ContactoAsociadoNavigation)
                .Include(c => c.ContraQuienNavigation)
                .Include(c => c.EtapaNavigation)
                .Include(c => c.MonedaNavigation)
                .Include(c => c.NombreCuentaNavigation)
                .Include(c => c.ProbabilidadNavigation)
                .Include(c => c.RazonDenegacionNavigation)
                .Include(c => c.SectorNavigation)
                .Include(c => c.TipoNavigation)
                .Include(c => c.ZonaNavigation)
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (cotizacione == null)
            {
                return NotFound();
            }

            return View(cotizacione);
        }


        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "IdContacto");
            ViewData["ContraQuien"] = new SelectList(_context.Rivales, "Id", "Id");
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Id", "Id");
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta");
            ViewData["Probabilidad"] = new SelectList(_context.Probabilidads, "Id", "Id");
            ViewData["Moneda"] = new SelectList(_context.Moneda, "Id", "Id");
            ViewData["RazonDenegacion"] = new SelectList(_context.CotizacionDenegada, "Id", "Id");
            ViewData["Tipo"] = new SelectList(_context.TipoCotizacions, "Id", "Id");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroCotizacion,NombreOportunidad,FechaCotizacion,FechaCierra,OrdenCompra,Descripcion,Factur,Zona,Sector,Moneda,ContactoAsociado,Asesor,NombreCuenta,Etapa,Probabilidad,Tipo,RazonDenegacion,ContraQuien")] Cotizacione cotizacione)
        {

            //UtiliZamos la funcion de encontrar contacto
            Contacto objeto = new log().EncontrarContacto(cotizacione.ContactoAsociado);



            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("agregarCotizacion", conexion);


                cmd.Parameters.AddWithValue("@numeroCot", cotizacione.NumeroCotizacion);
                cmd.Parameters.AddWithValue("@nombreOpor", cotizacione.NombreOportunidad);
                cmd.Parameters.AddWithValue("@fechaCot", cotizacione.FechaCotizacion);
                cmd.Parameters.AddWithValue("@fechaCierre", cotizacione.FechaCierra);
                cmd.Parameters.AddWithValue("@ordenCompra", cotizacione.OrdenCompra);
                cmd.Parameters.AddWithValue("@descripcion", cotizacione.Descripcion);
                cmd.Parameters.AddWithValue("@factura", cotizacione.Factur);
                cmd.Parameters.AddWithValue("@zona", objeto.Zona);
                cmd.Parameters.AddWithValue("@sector", objeto.Sector);
                cmd.Parameters.AddWithValue("@moneda", cotizacione.Moneda);
                cmd.Parameters.AddWithValue("@contactoAsociado", cotizacione.ContactoAsociado);
                cmd.Parameters.AddWithValue("@asesor", objeto.Asesor);
                cmd.Parameters.AddWithValue("@nombreCuenta", objeto.Cliente);
                cmd.Parameters.AddWithValue("@etapa", cotizacione.Etapa);
                cmd.Parameters.AddWithValue("@probabilidad", cotizacione.Probabilidad);
                cmd.Parameters.AddWithValue("@tipo", cotizacione.Tipo);
                cmd.Parameters.AddWithValue("@razon", cotizacione.RazonDenegacion);
                cmd.Parameters.AddWithValue("@contraQuien", cotizacione.ContraQuien);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return RedirectToAction("index", "Home");


            }
        }



        // GET: Cotizaciones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacione = await _context.Cotizaciones.FindAsync(id);
            if (cotizacione == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", cotizacione.Asesor);
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "IdContacto", cotizacione.ContactoAsociado);
            ViewData["ContraQuien"] = new SelectList(_context.Rivales, "Id", "Id", cotizacione.ContraQuien);
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Id", "Id", cotizacione.Etapa);
            ViewData["Moneda"] = new SelectList(_context.Moneda, "Id", "Id", cotizacione.Moneda);
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", cotizacione.NombreCuenta);
            ViewData["Probabilidad"] = new SelectList(_context.Probabilidads, "Id", "Id", cotizacione.Probabilidad);
            ViewData["RazonDenegacion"] = new SelectList(_context.CotizacionDenegada, "Id", "Id", cotizacione.RazonDenegacion);
            ViewData["Sector"] = new SelectList(_context.Sectors, "Id", "Id", cotizacione.Sector);
            ViewData["Tipo"] = new SelectList(_context.TipoCotizacions, "Id", "Id", cotizacione.Tipo);
            ViewData["Zona"] = new SelectList(_context.Zonas, "Id", "Id", cotizacione.Zona);
            return View(cotizacione);
        }

        // POST: Cotizaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroCotizacion,NombreOportunidad,FechaCotizacion,FechaCierra,OrdenCompra,Descripcion,Factur,Zona,Sector,Moneda,ContactoAsociado,Asesor,NombreCuenta,Etapa,Probabilidad,Tipo,RazonDenegacion,ContraQuien")] Cotizacione cotizacione)
        {
            using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
            {
                conexion.Open();


                SqlCommand cmd = new SqlCommand("editarCotizacion", conexion);


                cmd.Parameters.AddWithValue("@numeroCot", cotizacione.NumeroCotizacion);
                cmd.Parameters.AddWithValue("@nombreOportunidad", cotizacione.NombreOportunidad);
                cmd.Parameters.AddWithValue("@fechaCotizacion", cotizacione.FechaCotizacion);
                cmd.Parameters.AddWithValue("@fechaCierre", cotizacione.FechaCierra);
                cmd.Parameters.AddWithValue("@ordenCompra", cotizacione.OrdenCompra);
                cmd.Parameters.AddWithValue("@descripcion", cotizacione.Descripcion);
                cmd.Parameters.AddWithValue("@factura", cotizacione.Factur);
                cmd.Parameters.AddWithValue("@moneda", cotizacione.Moneda);
                cmd.Parameters.AddWithValue("@etapa", cotizacione.Etapa);
                cmd.Parameters.AddWithValue("@probabilidad", cotizacione.Probabilidad);
                cmd.Parameters.AddWithValue("@tipo", cotizacione.Tipo);
                cmd.Parameters.AddWithValue("@razon", cotizacione.RazonDenegacion);
                cmd.Parameters.AddWithValue("@contraQuien", cotizacione.ContraQuien);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return RedirectToAction("index", "Home");


            }
        }
    }
}

