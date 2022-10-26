using Microsoft.AspNetCore.Mvc;
using ProyectoCRM.Models;
using ProyectoCRM.logica;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ProyectoCRM.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string usuario, string clave)
        {
            Usuario objeto = new log().EncontrarUsuario(usuario, clave);

            if (objeto.Nombre != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, objeto.Cedula),
                    new Claim("username", objeto.NombreUsuario),
                    new Claim(ClaimTypes.Role, objeto.Rol.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



                return RedirectToAction("Index", "Home");


            }

            return View();

        }


        public async Task<IActionResult> Salir()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("Index");

        }
        
    }


    }

