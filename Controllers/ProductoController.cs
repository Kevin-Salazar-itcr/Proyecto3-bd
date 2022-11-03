using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;
using ProyectoCRM.Procesos;

namespace ProyectoCRM.Controllers
{

    [Authorize] 
    public class ProductoController : Controller
    {
        private readonly CRMContext _context;

        public ProductoController(CRMContext context)
        {
            _context = context;
        }


        productosProcesos _productosDatos = new productosProcesos();

        public IActionResult index()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = _productosDatos.Listar();

            return View(oLista);
        }



        //Funcion que prepara la vista para agregar productos
        [HttpGet]
        public IActionResult Create()

        {

            ProductoVM OproductoVM = new ProductoVM()
            {

                ObjProducto = new Producto(),
                ObjListaFamilia = _context.FamiliaProductos.Select(familia => new SelectListItem()
                {

                    Text = familia.Nombre,
                    Value = familia.Codigo.ToString()

                }).ToList()


            };

            return View(OproductoVM);

        }

        //Funcion que crea un producto en la base de datos
        //E: Un producto
        //S: El producto agregado en la base
        [HttpPost]
        public IActionResult Create(ProductoVM OproductoVM) {

            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("agregarProducto", conexion);


                    cmd.Parameters.AddWithValue("@codigo", OproductoVM.ObjProducto.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", OproductoVM.ObjProducto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", OproductoVM.ObjProducto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", OproductoVM.ObjProducto.Precio);
                    cmd.Parameters.AddWithValue("@activo", OproductoVM.ObjProducto.Activo);
                    cmd.Parameters.AddWithValue("@codigo_familia", OproductoVM.ObjProducto.CodigoFamilia);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();





                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Producto");


            }
            return RedirectToAction("index", "Producto");



        }

      
        //funcion que prepara la vista para editar productos
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CodigoFamilia"] = new SelectList(_context.FamiliaProductos, "Codigo", "Nombre", producto.CodigoFamilia);
            return View(producto);
        }



        //funcion que edita el producto en la base de datos
        //E: un id y un objeto producto
        //S: la edicion de algunos campos del producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Producto producto)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("editarProducto", conexion);


                    cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@activo", producto.Activo);
                    cmd.Parameters.AddWithValue("@familiaProducto", producto.CodigoFamilia);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();




                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Edit", "Producto");


            }


            return RedirectToAction("Index", "Producto");


        }

        }

    }



       













