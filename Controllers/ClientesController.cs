using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.Procesos;

namespace ProyectoCRM.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly CRMContext _context;

        public ClientesController(CRMContext context)
        {
            _context = context;
        }

        ClientesProcesos ClientesDatos = new ClientesProcesos();

        public async Task<IActionResult> Index()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = ClientesDatos.Listar();

            return View(oLista);

        }

        //Funcion que prepara la vista para agregar clientes, retorna al view de create
      
        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Nombre");
            ViewData["Idmoneda"] = new SelectList(_context.Moneda, "Id", "NombreMoneda");
            ViewData["Idsector"] = new SelectList(_context.Sectors, "Id", "Sector1");
            ViewData["Idzona"] = new SelectList(_context.Zonas, "Id", "Zona1");
            return View();
        }


        //Funcion para agregar cliente
        //E: Un objeto cliente
        //S: El objeto cliente agregado a la base de datos
        //R: Que el objeto cliente sea unico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Cliente cliente)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("agregarCliente", conexion);


                    cmd.Parameters.AddWithValue("@nombre_cuenta", cliente.NombreCuenta);
                    cmd.Parameters.AddWithValue("@celular", cliente.Celular);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@sitio", cliente.Sitio);
                    cmd.Parameters.AddWithValue("@contactoP", cliente.ContactoPrincipal);
                    cmd.Parameters.AddWithValue("@asesor", User.Identity.Name.ToString());
                    cmd.Parameters.AddWithValue("@zona", cliente.Idzona);
                    cmd.Parameters.AddWithValue("@sector", cliente.Idsector);
                    cmd.Parameters.AddWithValue("@moneda", cliente.Idmoneda);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Cliente");


            }

                return RedirectToAction("Index", "Cliente");
            }



        


        //Funcion que prepara la vista para eliminar, recibe el id del cliente
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.AsesorNavigation)
                .Include(c => c.IdmonedaNavigation)
                .Include(c => c.IdsectorNavigation)
                .Include(c => c.IdzonaNavigation)
                .FirstOrDefaultAsync(m => m.NombreCuenta == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

    
        //Funcion que elimina un cliente de la base de datos
        //E: un id de cliente
        //S: El cleinte eliminado de la base
        //R: Que el cliente no este asociado a ningun otro registro
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("eliminarCliente", conexion);


                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Index", "Cliente");


            }

            return RedirectToAction("Index", "Cliente");

        }

    }
}
