﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudDF3.Models;

namespace CrudDF3.Controllers
{
    public class ReservasController : Controller
    {
        private readonly CrudDf3Context _context;

        public ReservasController(CrudDf3Context context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var idUsuario = HttpContext.Session.GetString("idUsuario");
            var idRol = HttpContext.Session.GetString("idRol");

            IQueryable<Reserva> query = _context.Reservas;

            // Si es cliente (rol 2), filtrar solo sus reservas
            if (idRol == "2" && !string.IsNullOrEmpty(idUsuario))
            {
                query = query.Where(r => r.IdUsuario == int.Parse(idUsuario));
            }

            var reservas = await query
                .Include(r => r.IdUsuarioNavigation)
                .Include(r => r.ReservasPaquetes)
                .Select(r => new ReservaIndexViewModel
                {
                    IdReserva = r.IdReserva,
                    NombreUsuario = r.IdUsuarioNavigation.NombreUsuario,
                    FechaInicial = r.FechaInicial,
                    FechaFinal = r.FechaFinal,
                    NumeroPersonas = r.NumeroPersonas,
                    Valor = r.Valor,
                    Anticipo = r.Anticipo,
                    FechaReserva = r.FechaReserva,
                    EstadoReserva = r.EstadoReserva,
                    PaquetesCount = r.ReservasPaquetes.Count,
                    NombresPaquetes = string.Join(", ", r.ReservasPaquetes.Select(rp => rp.IdPaqueteNavigation.NombrePaquete))
                })
                .OrderByDescending(r => r.FechaReserva)
                .ToListAsync();

            return View(reservas);
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdUsuarioNavigation)
                .Include(r => r.ReservasPaquetes)
                    .ThenInclude(rp => rp.IdPaqueteNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create

        public IActionResult Create()
        {
            var model = new CreateReservaViewModel
            {
                Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario"),
                PaquetesDisponibles = _context.PaquetesTuristicos
                    .Where(p => p.DisponibilidadPaquete && p.EstadoPaquete)
                    .Select(p => new PaqueteSelectionViewModel
                    {
                        IdPaquete = p.IdPaquete,
                        NombrePaquete = p.NombrePaquete,
                        DescripcionPaquete = p.DescripcionPaquete,
                        PrecioPaquete = p.PrecioPaquete,
                        DestinoPaquete = p.DestinoPaquete,
                        TipoViajePaquete = p.TipoViajePaquete
                    }).ToList(),
                FechaReserva = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateReservaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Calcular el valor total basado en los paquetes seleccionados
                var valorTotal = model.PaquetesDisponibles
                    .Where(p => model.PaquetesSeleccionados.Contains(p.IdPaquete))
                    .Sum(p => p.PrecioPaquete) ?? 0;

                // Calcular el anticipo (30% del total)
                var anticipo = valorTotal * 0.3m;

                var reserva = new Reserva
                {
                    IdUsuario = model.IdUsuario,
                    FechaInicial = model.FechaInicial,
                    FechaFinal = model.FechaFinal,
                    NumeroPersonas = model.NumeroPersonas,
                    Valor = valorTotal, // Usar el valor calculado
                    Anticipo = anticipo, // Usar el anticipo calculado
                    FechaReserva = model.FechaReserva ?? DateTime.Now,
                    EstadoReserva = model.EstadoReserva
                };

                _context.Reservas.Add(reserva);
                _context.SaveChanges();

                // Agregar los paquetes seleccionados
                foreach (var paqueteId in model.PaquetesSeleccionados)
                {
                    _context.ReservasPaquetes.Add(new ReservasPaquete
                    {
                        IdReserva = reserva.IdReserva,
                        IdPaquete = paqueteId
                    });
                }

                _context.SaveChanges();

                return RedirectToAction("Details", new { id = reserva.IdReserva });
            }

            // Repoblar datos si hay error
            model.Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", model.IdUsuario);
            model.PaquetesDisponibles = _context.PaquetesTuristicos
                .Where(p => p.DisponibilidadPaquete && p.EstadoPaquete)
                .Select(p => new PaqueteSelectionViewModel
                {
                    IdPaquete = p.IdPaquete,
                    NombrePaquete = p.NombrePaquete,
                    DescripcionPaquete = p.DescripcionPaquete,
                    PrecioPaquete = p.PrecioPaquete,
                    DestinoPaquete = p.DestinoPaquete,
                    TipoViajePaquete = p.TipoViajePaquete
                }).ToList();

            return View(model);
        }

        // POST: Reservas/Creat


        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.ReservasPaquetes)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            var viewModel = new EditReservaViewModel
            {
                IdReserva = reserva.IdReserva,
                IdUsuario = reserva.IdUsuario,
                FechaInicial = reserva.FechaInicial,
                FechaFinal = reserva.FechaFinal,
                NumeroPersonas = reserva.NumeroPersonas,
                Valor = reserva.Valor,
                Anticipo = reserva.Anticipo,
                FechaReserva = reserva.FechaReserva,
                EstadoReserva = reserva.EstadoReserva,
                PaquetesSeleccionados = reserva.ReservasPaquetes.Select(rp => rp.IdPaquete).ToList(),
                Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", reserva.IdUsuario),
                PaquetesDisponibles = await _context.PaquetesTuristicos
                    .Where(p => p.DisponibilidadPaquete && p.EstadoPaquete)
                    .Select(p => new PaqueteSelectionViewModel
                    {
                        IdPaquete = p.IdPaquete,
                        NombrePaquete = p.NombrePaquete,
                        DescripcionPaquete = p.DescripcionPaquete,
                        PrecioPaquete = p.PrecioPaquete,
                        DestinoPaquete = p.DestinoPaquete,
                        TipoViajePaquete = p.TipoViajePaquete
                    }).ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditReservaViewModel viewModel)
        {
            if (id != viewModel.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var reserva = await _context.Reservas
                        .Include(r => r.ReservasPaquetes)
                        .FirstOrDefaultAsync(r => r.IdReserva == id);

                    if (reserva == null)
                    {
                        return NotFound();
                    }

                    // Actualizar propiedades básicas
                    reserva.IdUsuario = viewModel.IdUsuario;
                    reserva.FechaInicial = viewModel.FechaInicial;
                    reserva.FechaFinal = viewModel.FechaFinal;
                    reserva.NumeroPersonas = viewModel.NumeroPersonas;

                    // Recalcular valor basado en paquetes seleccionados
                    var valorTotal = viewModel.PaquetesDisponibles?
                        .Where(p => viewModel.PaquetesSeleccionados.Contains(p.IdPaquete))
                        .Sum(p => p.PrecioPaquete) ?? 0;

                    reserva.Valor = valorTotal;
                    reserva.Anticipo = viewModel.Anticipo;
                    reserva.FechaReserva = viewModel.FechaReserva;
                    reserva.EstadoReserva = viewModel.EstadoReserva;

                    // Manejar paquetes
                    await ActualizarPaquetesReserva(reserva.IdReserva, viewModel.PaquetesSeleccionados);

                    _context.Update(reserva);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(viewModel.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Si hay errores, repoblar los datos necesarios
            viewModel.Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", viewModel.IdUsuario);
            viewModel.PaquetesDisponibles = await _context.PaquetesTuristicos
                .Where(p => p.DisponibilidadPaquete && p.EstadoPaquete)
                .Select(p => new PaqueteSelectionViewModel
                {
                    IdPaquete = p.IdPaquete,
                    NombrePaquete = p.NombrePaquete,
                    DescripcionPaquete = p.DescripcionPaquete,
                    PrecioPaquete = p.PrecioPaquete,
                    DestinoPaquete = p.DestinoPaquete,
                    TipoViajePaquete = p.TipoViajePaquete
                }).ToListAsync();

            return View(viewModel);
        }

        private async Task ActualizarPaquetesReserva(int idReserva, List<int> selectedPaquetes)
        {
            var paquetesActuales = await _context.ReservasPaquetes
                .Where(rp => rp.IdReserva == idReserva)
                .ToListAsync();

            // Eliminar relaciones removidas
            foreach (var paquete in paquetesActuales)
            {
                if (!selectedPaquetes.Contains(paquete.IdPaquete))
                    _context.ReservasPaquetes.Remove(paquete);
            }

            // Agregar nuevas relaciones
            foreach (var paqueteId in selectedPaquetes)
            {
                if (!paquetesActuales.Any(p => p.IdPaquete == paqueteId))
                    _context.ReservasPaquetes.Add(new ReservasPaquete { IdReserva = idReserva, IdPaquete = paqueteId });
            }
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdUsuarioNavigation)
                .Include(r => r.ReservasPaquetes)
                    .ThenInclude(rp => rp.IdPaqueteNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.ReservasPaquetes)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva != null)
            {
                _context.ReservasPaquetes.RemoveRange(reserva.ReservasPaquetes);
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}