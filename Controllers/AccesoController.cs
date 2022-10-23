using Microsoft.AspNetCore.Mvc;
using ProyectoCRM.Models;
using ProyectoCRM.logica;


namespace ProyectoCRM.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, string clave)
        {
            usuario objeto = new log().EncontrarUsuario(usuario, clave);

            if (objeto.nombre != null)
            {



                return RedirectToAction("Index", "Home");


                
            }
            return View();

        }
    }
}
