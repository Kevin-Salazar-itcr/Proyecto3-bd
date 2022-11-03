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
        //Funcion que retorna a la pantalla de acceso
        public IActionResult Index()
        {
            return View();
        }

        //Funcion encargada de validar si el usuario existe en la base de datos
        //E: Un usuario y una clave
        //S: Si el usuario es valido retorna a la pagina de inicio
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string usuario, string clave)
        {


            try
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
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Edit", "Producto");


            }

            return View();

        }

        //Funcion que cierra sesion, retorna al login de inicio

        public async Task<IActionResult> Salir()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("Index");

        }
        
    }


    }

