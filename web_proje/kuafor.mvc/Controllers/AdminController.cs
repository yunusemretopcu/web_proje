using kuafor.mvc.Models;
using kuafor.mvc.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            return View();
        }

        // GET: Admin/ManageSalons
        public IActionResult ManageSalons()
        {
            var salons = _context.Salons.ToList();
            ViewBag.Salons = salons;
            return View();
        }

        // GET: Admin/ManageEmployees
        public IActionResult ManageEmployees()
        {
            var employees = _context.Employees
                                    .Include(e => e.Salon)
                                    .ToList();
            ViewBag.Employees = employees;
            return View();
        }

        // GET: Admin/ManageAppointments
        public IActionResult ManageAppointments()
        {
            var appointments = _context.Appointments
                                       .Include(a => a.Customer)
                                       .Include(a => a.Employee)
                                       .Include(a => a.Service)
                                       .ToList();
            ViewBag.Appointments = appointments;
            return View();
        }

        // GET: Admin/Logout
        public IActionResult Logout()
        {
            // Admin oturumunu kapatma işlemi (örneğin session temizliği)
            return RedirectToAction("Login");
        }
    }
}
