// PaquetesTuristicoesController.cs
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
    public class PaquetesTuristicoesController : Controller
    {
        private readonly CrudDf3Context _context;

        public PaquetesTuristicoesController(CrudDf3Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexPublic()
        {
            var paquetes = await _context.PaquetesTuristicos
                .Where(p => p.EstadoPaquete && p.DisponibilidadPaquete) // Solo paquetes activos y disponibles
                .Include(p => p.PaqueteHabitaciones)
                .ThenInclude(ph => ph.IdHabitacionNavigation)
                .Include(p => p.PaqueteServicios)
                .ThenInclude(ps => ps.IdServicioNavigation)
                .ToListAsync();

            return View(paquetes);
        }

        [HttpGet]
        public IActionResult VerificarStock(int id)
        {
            var paquete = _context.PaquetesTuristicos.Find(id);
            if (paquete == null)
            {
                return Json(new { stock = 0, nombre = "Paquete no encontrado" });
            }
            return Json(new
            {
                stock = paquete.StockPaquete,
                nombre = paquete.NombrePaquete,
                disponible = paquete.DisponibilidadPaquete
            });
        }

        // GET: PaquetesTuristicoes
        public async Task<IActionResult> Index()
        {
            var paquetes = await _context.PaquetesTuristicos
                .Include(p => p.PaqueteServicios)
                    .ThenInclude(ps => ps.IdServicioNavigation)
                .Include(p => p.PaqueteHabitaciones)
                    .ThenInclude(ph => ph.IdHabitacionNavigation)
                .AsNoTracking()
                .ToListAsync();

            return View(paquetes);
        }

        // GET: PaquetesTuristicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.PaquetesTuristicos
                .Include(p => p.PaqueteServicios)
                    .ThenInclude(ps => ps.IdServicioNavigation)
                .Include(p => p.PaqueteHabitaciones)
                    .ThenInclude(ph => ph.IdHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdPaquete == id);

            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // GET: PaquetesTuristicoes/Create
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                // ✅ Verificación explícita y manejo de posibles nulos
                var servicios = _context.Servicios?.ToList() ?? new List<Servicio>();
                var habitaciones = _context.Habitaciones?.ToList() ?? new List<Habitacione>();

                ViewData["Servicios"] = servicios;
                ViewData["Habitaciones"] = habitaciones;

                return View(new PaquetesTuristico()); // ← Asegúrate de pasar un modelo nuevo
            }
            catch (Exception ex)
            {
                // Loggear el error (opcional)
                Console.WriteLine($"Error al cargar datos: {ex.Message}");

                // Retornar listas vacías para evitar errores en la vista
                ViewData["Servicios"] = new List<Servicio>();
                ViewData["Habitaciones"] = new List<Habitacione>();
                return View(new PaquetesTuristico());
            }
        }

        // POST: PaquetesTuristicoes/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdPaquete,NombrePaquete,DescripcionPaquete,PrecioPaquete,DisponibilidadPaquete,FechaPaquete,DestinoPaquete,EstadoPaquete,TipoViajePaquete,StockPaquete,CapacidadPaquete,SelectedServicios,SelectedHabitaciones")] PaquetesTuristico paquete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paquete);
                await _context.SaveChangesAsync();

                // Calcular capacidad basada en las habitaciones seleccionadas
                if (paquete.SelectedHabitaciones != null)
                {
                    int capacidadTotal = 0;
                    foreach (var habitacionId in paquete.SelectedHabitaciones)
                    {
                        var habitacion = await _context.Habitaciones.FindAsync(habitacionId);
                        if (habitacion != null)
                        {
                            capacidadTotal += habitacion.CapacidadHuespedes;

                            _context.PaqueteHabitaciones.Add(new PaqueteHabitacion
                            {
                                IdPaquete = paquete.IdPaquete,
                                IdHabitacion = habitacionId
                            });
                        }
                    }
                    paquete.CapacidadPaquete = capacidadTotal;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Servicios"] = _context.Servicios.ToList();
            ViewData["Habitaciones"] = _context.Habitaciones.ToList();
            return View(paquete);
        }

        // GET: PaquetesTuristicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.PaquetesTuristicos
                .Include(p => p.PaqueteServicios)
                .Include(p => p.PaqueteHabitaciones)
                .FirstOrDefaultAsync(p => p.IdPaquete == id);

            if (paquete == null)
            {
                return NotFound();
            }

            paquete.SelectedServicios = paquete.PaqueteServicios.Select(ps => ps.IdServicio).ToList();
            paquete.SelectedHabitaciones = paquete.PaqueteHabitaciones.Select(ph => ph.IdHabitacion).ToList();

            ViewData["Servicios"] = _context.Servicios.ToList();
            ViewData["Habitaciones"] = _context.Habitaciones.ToList();
            return View(paquete);
        }

        // POST: PaquetesTuristicoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaquete,NombrePaquete,DescripcionPaquete,PrecioPaquete,DisponibilidadPaquete,FechaPaquete,DestinoPaquete,EstadoPaquete,TipoViajePaquete,StockPaquete,CapacidadPaquete,SelectedServicios,SelectedHabitaciones")] PaquetesTuristico paquete)
        {
            if (id != paquete.IdPaquete)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (paquete.SelectedHabitaciones != null && paquete.SelectedHabitaciones.Any())
                    {
                        int capacidadTotal = 0;
                        var habitaciones = await _context.Habitaciones
                            .Where(h => paquete.SelectedHabitaciones.Contains(h.IdHabitacion))
                            .ToListAsync();

                        foreach (var habitacion in habitaciones)
                        {
                            capacidadTotal += habitacion.CapacidadHuespedes;
                        }
                        paquete.CapacidadPaquete = capacidadTotal;
                    }
                    else
                    {
                        paquete.CapacidadPaquete = 0;
                    }
                    // Asegurar que la disponibilidad sea coherente con el stock
                    ActualizarDisponibilidadSegunStock(paquete);

                    // Actualizar el paquete
                    _context.Update(paquete);

                    // Actualizar servicios
                    var serviciosActuales = await _context.PaqueteServicios
                        .Where(ps => ps.IdPaquete == id)
                        .ToListAsync();

                    // Eliminar servicios no seleccionados
                    foreach (var servicio in serviciosActuales)
                    {
                        if (!paquete.SelectedServicios.Contains(servicio.IdServicio))
                        {
                            _context.PaqueteServicios.Remove(servicio);
                        }
                    }

                    // Agregar nuevos servicios seleccionados
                    foreach (var servicioId in paquete.SelectedServicios)
                    {
                        if (!serviciosActuales.Any(ps => ps.IdServicio == servicioId))
                        {
                            _context.PaqueteServicios.Add(new PaqueteServicio
                            {
                                IdPaquete = paquete.IdPaquete,
                                IdServicio = servicioId
                            });
                        }
                    }

                    // Actualizar habitaciones
                    var habitacionesActuales = await _context.PaqueteHabitaciones
                        .Where(ph => ph.IdPaquete == id)
                        .ToListAsync();

                    // Eliminar habitaciones no seleccionadas
                    foreach (var habitacion in habitacionesActuales)
                    {
                        if (!paquete.SelectedHabitaciones.Contains(habitacion.IdHabitacion))
                        {
                            _context.PaqueteHabitaciones.Remove(habitacion);
                        }
                    }

                    // Agregar nuevas habitaciones seleccionadas
                    foreach (var habitacionId in paquete.SelectedHabitaciones)
                    {
                        if (!habitacionesActuales.Any(ph => ph.IdHabitacion == habitacionId))
                        {
                            _context.PaqueteHabitaciones.Add(new PaqueteHabitacion
                            {
                                IdPaquete = paquete.IdPaquete,
                                IdHabitacion = habitacionId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaqueteTuristicoExists(paquete.IdPaquete))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Servicios"] = _context.Servicios.ToList();
            ViewData["Habitaciones"] = _context.Habitaciones.ToList();
            return View(paquete);
        }

        // GET: PaquetesTuristicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.PaquetesTuristicos
                .Include(p => p.PaqueteServicios)
                    .ThenInclude(ps => ps.IdServicioNavigation)
                .Include(p => p.PaqueteHabitaciones)
                    .ThenInclude(ph => ph.IdHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdPaquete == id);

            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // POST: PaquetesTuristicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paquete = await _context.PaquetesTuristicos
                .Include(p => p.PaqueteServicios)
                .Include(p => p.PaqueteHabitaciones)
                .FirstOrDefaultAsync(p => p.IdPaquete == id);

            if (paquete != null)
            {
                // Eliminar relaciones primero
                _context.PaqueteServicios.RemoveRange(paquete.PaqueteServicios);
                _context.PaqueteHabitaciones.RemoveRange(paquete.PaqueteHabitaciones);

                // Luego eliminar el paquete
                _context.PaquetesTuristicos.Remove(paquete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteTuristicoExists(int id)
        {
            return _context.PaquetesTuristicos.Any(e => e.IdPaquete == id);
        }

        // Método privado para actualizar la disponibilidad según el stock
        private void ActualizarDisponibilidadSegunStock(PaquetesTuristico paquete)
        {
            // Si el stock es 0, desactivar la disponibilidad
            if (paquete.StockPaquete <= 0)
            {
                paquete.DisponibilidadPaquete = false;
            }
            // Si el stock es mayor que 0 y el paquete está activo, asegurarse de que esté disponible
            else if (paquete.EstadoPaquete)
            {
                paquete.DisponibilidadPaquete = true;
            }
        }
    }
}