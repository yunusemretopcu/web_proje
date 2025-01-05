using kuafor.api.Context;
using Microsoft.AspNetCore.Mvc;

namespace kuafor.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("AdminAppointments")]
        public IActionResult GetAdminAppointments()
        {
            var appointments = _context.Appointments.ToList();
            return Ok(appointments); // JSON formatında döndürülür
        }
    }

}
