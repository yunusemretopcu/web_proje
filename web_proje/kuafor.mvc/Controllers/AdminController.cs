using kuafor.mvc.Models;
using kuafor.mvc.Context;
using Microsoft.AspNetCore.Mvc;

namespace kuafor.mvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
            if (admin != null)
            {
                // Admin girişi başarılı
                return RedirectToAction("Index", "Salon");
            }

            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            return View();
        }
    }
}
