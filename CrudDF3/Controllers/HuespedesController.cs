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
    public class HuespedesController : Controller
    {
        private readonly CrudDf3Context _context;

        public HuespedesController(CrudDf3Context context)
        {
            _context = context;
        }

        // GET: Huespedes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Huespedes.ToListAsync());
        }

        // GET: Huespedes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespede = await _context.Huespedes
                .FirstOrDefaultAsync(m => m.IdHuesped == id);
            if (huespede == null)
            {
                return NotFound();
            }

            return View(huespede);
        }

        // GET: Huespedes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Huespedes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHuesped,CedulaHuesped,NombreHuesped,ApellidoHuesped,CorreoHuesped,FechaEntradaHuesped,FechaSalidaHuesped")] Huespede huespede)
        {
            if (ModelState.IsValid)
            {
                _context.Add(huespede);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(huespede);
        }

        // GET: Huespedes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespede = await _context.Huespedes.FindAsync(id);
            if (huespede == null)
            {
                return NotFound();
            }
            return View(huespede);
        }

        // POST: Huespedes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHuesped,CedulaHuesped,NombreHuesped,ApellidoHuesped,CorreoHuesped,FechaEntradaHuesped,FechaSalidaHuesped")] Huespede huespede)
        {
            if (id != huespede.IdHuesped)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(huespede);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HuespedeExists(huespede.IdHuesped))
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
            return View(huespede);
        }

        // GET: Huespedes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespede = await _context.Huespedes
                .FirstOrDefaultAsync(m => m.IdHuesped == id);
            if (huespede == null)
            {
                return NotFound();
            }

            return View(huespede);
        }

        // POST: Huespedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var huespede = await _context.Huespedes.FindAsync(id);
            if (huespede != null)
            {
                _context.Huespedes.Remove(huespede);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HuespedeExists(int id)
        {
            return _context.Huespedes.Any(e => e.IdHuesped == id);
        }
    }
}
