using kuafor.mvc.Context;
using kuafor.mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kuafor.mvc.Controllers
{
    
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public AppointmentController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAppointments()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7105"); // Web API'nin URL'sini buraya yazın

            var response = await httpClient.GetAsync("api/Appointment/AdminAppointments");
            if (response.IsSuccessStatusCode)
            {
                var appointments = await response.Content.ReadFromJsonAsync<IEnumerable<Appointment>>();
                return View(appointments);
            }
            else
            {
                // Hata durumunda kullanıcıya bilgi verin
                TempData["Error"] = "Randevular getirilemedi!";
                return RedirectToAction("Index", "Home");
            }

        }


        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var userId = User.Identity.Name; // Oturum açmış kullanıcının kimliği

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı kimliği bulunamadı.");
            }
            var appointments = _context.Appointments.Where(a=>a.UserId == userId).Include(a => a.Employee).ToList();
            return View(appointments);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.Where(e => e.IsAvailable).ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Create(Appointment appointment)
        {
            
            var day = appointment.AppointmentTime.DayOfWeek;
            var hour = appointment.AppointmentTime.Hour;
            if (hour < 9 || hour > 19) { 
                ModelState.AddModelError("","Bu saatler arasında hizmet verememekteyiz");
                ViewBag.Employees = _context.Employees.Where(e => e.IsAvailable).ToList();
                return View(appointment);
            }
            if (day == DayOfWeek.Sunday || day == DayOfWeek.Saturday) { 
                ModelState.AddModelError("", "Haftasonu hizmet verememekteyiz");
                ViewBag.Employees = _context.Employees.Where(e => e.IsAvailable).ToList();
                return View(appointment);
            }

            // Kullanıcının aynı tarih ve saatte randevusu olup olmadığını kontrol et
            bool isExistingAppointment = _context.Appointments
                .Any(a => a.UserId == appointment.UserId && a.AppointmentTime == appointment.AppointmentTime);

            if (isExistingAppointment)
            {
                // Hata mesajı döndür
                ModelState.AddModelError("", "Bu tarihe zaten bir randevunuz var. Lütfen başka bir tarih seçin.");
                ViewBag.Employees = _context.Employees.Where(e => e.IsAvailable).ToList();
                return View(appointment);
            }

            appointment.UserId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employees = _context.Employees.Where(e => e.IsAvailable).ToList();
            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
