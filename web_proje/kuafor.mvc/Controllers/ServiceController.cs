using kuafor.mvc.Models;
using kuafor.mvc.Context;
using kuafor.mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kuafor.mvc.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Service
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services.Include(s => s.Salon).ToListAsync();
            return View(services);
        }

        // GET: Service/Create
        public IActionResult Create()
        {
            ViewData["Salons"] = _context.Salons.ToList(); // Drop-down için
            return View();
        }

        // POST: Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // CRUD işlemlerini SalonController'dakine benzer şekilde devam ettirin.
    }
}
