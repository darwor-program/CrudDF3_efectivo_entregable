using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudDF3.Models;

namespace CrudDF3.Controllers
{
    public class RolesController : Controller
    {
        private readonly CrudDf3Context _context;

        public RolesController(CrudDf3Context context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .Include(r => r.RolPermisos)
                .ThenInclude(rp => rp.IdPermisoNavigation) // Incluimos los permisos asociados
                .FirstOrDefaultAsync(m => m.IdRol == id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            // Obtener la lista de permisos disponibles para asignar al rol
            var permisos = _context.Permisos
                .Select(p => new SelectListItem
                {
                    Value = p.IdPermiso.ToString(),
                    Text = p.NombrePermiso
                }).ToList();

            var model = new CreateRoleViewModel
            {
                Permissions = permisos
            };

            return View(model);
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Crear el rol
                var role = new Role
                {
                    NombreRol = model.NombreRol,
                    DescripcionRol = model.DescripcionRol,
                    EstadoRol = model.EstadoRol
                };

                // Agregar el rol a la base de datos
                _context.Add(role);
                await _context.SaveChangesAsync();

                // Asignar los permisos seleccionados al rol
                if (model.SelectedPermissions != null)
                {
                    foreach (var permissionId in model.SelectedPermissions)
                    {
                        var rolPermiso = new RolPermiso
                        {
                            IdRol = role.IdRol,
                            IdPermiso = permissionId
                        };
                        _context.Add(rolPermiso);
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, pasar de nuevo la lista de permisos
            var permisos = _context.Permisos
                .Select(p => new SelectListItem
                {
                    Value = p.IdPermiso.ToString(),
                    Text = p.NombrePermiso
                }).ToList();

            model.Permissions = permisos;
            return View(model);
        }

        // GET: Roles/Edit/5


        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditRoleViewModel model)
        {
            if (id != model.IdRol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var role = await _context.Roles
                    .Include(r => r.RolPermisos)
                    .FirstOrDefaultAsync(r => r.IdRol == id);

                if (role == null)
                {
                    return NotFound();
                }

                // Eliminar los permisos existentes
                role.RolPermisos.Clear();

                // Agregar los permisos seleccionados
                foreach (var permisoId in model.SelectedPermissions)
                {
                    role.RolPermisos.Add(new RolPermiso
                    {
                        IdRol = role.IdRol,
                        IdPermiso = permisoId
                    });
                }

                // Guardar los cambios
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si llegamos aquí es porque hubo un error
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await _context.Roles
                .Include(r => r.RolPermisos)
                .ThenInclude(rp => rp.IdPermisoNavigation)
                .FirstOrDefaultAsync(r => r.IdRol == id);

            if (role == null)
            {
                return NotFound();
            }

            // Obtener la lista de permisos disponibles
            var allPermissions = await _context.Permisos.ToListAsync();

            // Obtener los permisos ya asignados a este rol
            var selectedPermissions = role.RolPermisos.Select(rp => rp.IdPermiso).Cast<int>().ToList();

            var model = new EditRoleViewModel
            {
                IdRol = role.IdRol,
                NombreRol = role.NombreRol,
                DescripcionRol = role.DescripcionRol,
                EstadoRol = role.EstadoRol,
                AllPermissions = allPermissions,
                SelectedPermissions = selectedPermissions
            };

            return View(model);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.IdRol == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.IdRol == id);
        }
    }
}
