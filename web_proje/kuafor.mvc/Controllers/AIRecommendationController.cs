using kuafor.mvc.Models;
using kuafor.mvc.Context;
using kuafor.mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace kuafor.mvc.Controllers
{
    public class AIRecommendationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AIRecommendationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AIRecommendation/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: AIRecommendation/GetRecommendation
        [HttpPost]
        public IActionResult GetRecommendation(int customerId, IFormFile photo)
        {
            if (photo != null && customerId > 0)
            {
                // Yapay zeka ile öneriler işlenecek (simülasyon için bir JSON dönebilir).
                var recommendations = new List<string> { "Short Hair", "Blonde Highlights", "Layered Cut" };

                // Veritabanına kaydet
                var recommendation = new AIRecommendation
                {
                    CustomerId = customerId,
                    UploadedPhoto = ConvertFileToByteArray(photo),
                    RecommendedStyles = string.Join(", ", recommendations)
                };

                _context.AIRecommendations.Add(recommendation);
                _context.SaveChanges();

                return View("RecommendationResult", recommendations);
            }

            ModelState.AddModelError("", "Lütfen geçerli bir fotoğraf yükleyin ve müşteri seçin.");
            return View("Index");
        }

        private byte[] ConvertFileToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
