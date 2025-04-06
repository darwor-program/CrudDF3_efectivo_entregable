using CrudDF3.Models;
using CrudDF3.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace CrudDF3.Controllers
{
    public class AccountController : Controller
    {
        private readonly CrudDf3Context _context;

        public AccountController(CrudDf3Context context)
        {
            _context = context;

        }

        // MÉTODO PARA MOSTRAR EL FORMULARIO DE LOGIN
        public IActionResult Login()
        {
            return View();
        }

        // MÉTODO PARA PROCESAR EL LOGIN
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {    
                var user = _context.Usuarios
                    .FirstOrDefault(u => u.CorreoUsuario == model.CorreoUsuario && u.ContraseñaUsuario == model.ContraseñaUsuario);

                if (user != null)
                {
                    // GUARDAR DATOS EN LA SESIÓN
                    HttpContext.Session.SetString("idUsuario", user.IdUsuario.ToString());
                    HttpContext.Session.SetString("nombreUsuario", user.NombreUsuario);
                    HttpContext.Session.SetString("idRol", user.IdRol.ToString());
                    
                    return RedirectToAction("Index", "Home"); // REDIRIGIR A LA PÁGINA PRINCIPAL
                }
                else
                {
                    ViewBag.ErrorMessage = "Usuario o contraseña incorrectos";
                }

            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }
        // MÉTODO PARA REGISTRO, REUTILIZANDO "Create" DE UsuarioController
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                // VALIDAR QUE EL CORREO NO EXISTA YA EN LA BASE DE DATOS
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoUsuario == model.CorreoUsuario);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("", "Este correo ya está registrado.");
                    return View(model);
                }

                // CREAR NUEVO USUARIO CON LOS DATOS DEL FORMULARIO
                var usuario = new Usuario
                {
                    CedulaUsuario = model.CedulaUsuario,
                    NombreUsuario = model.NombreUsuario,
                    ApellidoUsuario = model.ApellidoUsuario,
                    CorreoUsuario = model.CorreoUsuario,
                    Telefono = model.Telefono,
                    Direccion = model.Direccion,
                    ContraseñaUsuario = model.ContraseñaUsuario, // SE RECOMIENDA HASHEAR LA CONTRASEÑA
                    IdRol = 2, // CLIENTE
                    EstadoUsuario = true,
                    FechaCreacion = DateTime.Now
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login"); // REDIRIGE AL LOGIN TRAS REGISTRARSE
            }

            return View(model);
        }



        // MÉTODO PARA CERRAR SESIÓN
        public IActionResult Logout()
        {
            // LIMPIAR LA SESIÓN
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("IdUsuario");

            // REDIRIGIR AL LOGIN
            return RedirectToAction("Login", "Account");
        }


    }
}
