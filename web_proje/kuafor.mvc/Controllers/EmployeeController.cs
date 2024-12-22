using kuafor.mvc.Models;
using kuafor.mvc.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kuafor.mvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Include(e => e.Salon).ToListAsync();
            return View(employees);
        }

        // CRUD işlemleri diğer controller yapısına benzer şekilde devam edecektir.
    }
}
