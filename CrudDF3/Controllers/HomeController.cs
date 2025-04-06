using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudDF3.Models;

namespace CrudDF3.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrudDf3Context _context;

        public HomeController(CrudDf3Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var paquetes = _context.PaquetesTuristicos
                .Include(p => p.PaqueteServicios)
                    .ThenInclude(ps => ps.IdServicioNavigation)
                .Include(p => p.PaqueteHabitaciones)
                    .ThenInclude(ph => ph.IdHabitacionNavigation)
                .ToList();

            return View(paquetes);
        }
    }
}
