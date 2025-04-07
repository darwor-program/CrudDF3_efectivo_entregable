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

        // MÉTODO PARA PROCESAR EL LOGIN (CORREGIDO)
        // MÉTODO PARA PROCESAR EL LOGIN (VERSIÓN CORREGIDA)
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Usuarios
                    .Include(u => u.IdRolNavigation) // INCLUYE LA RELACIÓN CON ROLES
                    .FirstOrDefault(u => u.CorreoUsuario == model.CorreoUsuario);

                if (user == null)
                {
                    ViewBag.ErrorMessage = "El correo no está registrado.";
                    return View(model);
                }

                // COMPARAR CONTRASEÑA (SI USAS HASH, DEBES VERIFICARLO CON BCrypt O Similar)
                if (user.ContraseñaUsuario != model.ContraseñaUsuario)
                {
                    ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                    return View(model);
                }

                // GUARDAR DATOS EN SESIÓN
                HttpContext.Session.SetString("idUsuario", user.IdUsuario.ToString());
                HttpContext.Session.SetString("nombreUsuario", user.NombreUsuario);
                HttpContext.Session.SetString("idRol", user.IdRol.ToString());

                // VERIFICAR QUE EL ROL NO SEA NULO
                if (user.IdRolNavigation != null)
                {
                    HttpContext.Session.SetString("nombreRol", user.IdRolNavigation.NombreRol);
                }

                return RedirectToAction("Index", "Home"); // REDIRIGIR A HOME
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        // MÉTODO PARA REGISTRO (CORREGIDO)
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                // VALIDAR QUE EL CORREO NO EXISTA YA EN LA BASE DE DATOS
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.CorreoUsuario == model.CorreoUsuario);

                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("CorreoUsuario", "Este correo ya está registrado.");
                    return View(model);
                }

                // BUSCAR EL ROL POR SU NOMBRE
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == model.NombreRol);

                // SI NO EXISTE, LO CREA
                if (rol == null)
                {
                    rol = new Role
                    {
                        NombreRol = model.NombreRol,
                        DescripcionRol = $"Rol {model.NombreRol}",
                        EstadoRol = true
                    };

                    _context.Roles.Add(rol);
                    await _context.SaveChangesAsync();
                }

                // CREAR NUEVO USUARIO
                var usuario = new Usuario
                {
                    CedulaUsuario = model.CedulaUsuario,
                    NombreUsuario = model.NombreUsuario,
                    ApellidoUsuario = model.ApellidoUsuario,
                    CorreoUsuario = model.CorreoUsuario,
                    Telefono = model.Telefono,
                    Direccion = model.Direccion,
                    ContraseñaUsuario = model.ContraseñaUsuario, // HASHEAR EN PRODUCCIÓN
                    IdRol = rol.IdRol,
                    EstadoUsuario = true,
                    FechaCreacion = DateTime.Now
                };

                _context.Usuarios.Add(usuario);

                try
                {
                    await _context.SaveChangesAsync();
                    TempData["RegistroExitoso"] = "Registro completado con éxito. Por favor inicie sesión.";
                    return RedirectToAction("Login");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Error al registrar el usuario: " + ex.Message);
                }
            }

            return View(model);
        }


        // MÉTODO PARA CERRAR SESIÓN (CORREGIDO)
        public IActionResult Logout()
        {
            // Limpiar todos los valores de sesión de forma segura
            HttpContext.Session.Remove("idUsuario");
            HttpContext.Session.Remove("nombreUsuario");
            HttpContext.Session.Remove("idRol");
            HttpContext.Session.Remove("nombreRol");

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Recuperar(string correo)
        {
            // Verificar si el correo existe
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoUsuario == correo);
            if (usuario == null)
            {
                ViewBag.ErrorMessage = "El correo no está registrado";
                return View();
            }

            // Generar código de 6 dígitos
            Random rnd = new();
            int codigo = rnd.Next(100000, 999999);

            // Guardar en sesión
            HttpContext.Session.SetString("CorreoRecuperacion", correo);
            HttpContext.Session.SetInt32("CodigoRecuperacion", codigo);

            // Mostrar código en la siguiente vista
            TempData["CodigoGenerado"] = codigo;
            return RedirectToAction("VerificarCodigo");
        }

        // Paso 2: Vista para verificar código
        public IActionResult VerificarCodigo()
        {
            if (HttpContext.Session.GetString("CorreoRecuperacion") == null)
            {
                return RedirectToAction("Recuperar");
            }

            // Mostrar código generado (solo para desarrollo)
            if (TempData.ContainsKey("CodigoGenerado"))
            {
                ViewBag.CodigoGenerado = TempData["CodigoGenerado"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult VerificarCodigo(int codigoIngresado)
        {
            int? codigoGuardado = HttpContext.Session.GetInt32("CodigoRecuperacion");

            if (codigoGuardado == null || codigoGuardado != codigoIngresado)
            {
                ViewBag.ErrorMessage = "Código incorrecto";
                ViewBag.CodigoGenerado = codigoGuardado; // Mostrar nuevamente el código
                return View();
            }

            return RedirectToAction("NuevaContraseña");
        }

        // Paso 3: Vista para nueva contraseña
        public IActionResult NuevaContraseña()
        {
            if (HttpContext.Session.GetString("CorreoRecuperacion") == null)
            {
                return RedirectToAction("Recuperar");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevaContraseña(string nuevaContraseña)
        {
            string correo = HttpContext.Session.GetString("CorreoRecuperacion");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoUsuario == correo);
            if (usuario != null)
            {
                usuario.ContraseñaUsuario = nuevaContraseña; // Deberías hashear esto en producción
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }

            // Limpiar sesión
            HttpContext.Session.Remove("CorreoRecuperacion");
            HttpContext.Session.Remove("CodigoRecuperacion");

            TempData["SuccessMessage"] = "Contraseña actualizada correctamente";
            return RedirectToAction("Login");
        }
    }
}

