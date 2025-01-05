using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace kuafor.mvc.Controllers
{
    public class AIController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AIController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult GetSuggestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetSuggestion(string inputDescription)
        {
            if (string.IsNullOrWhiteSpace(inputDescription))
            {
                ViewBag.Suggestion = "Lütfen bir açıklama girin.";
                return View();
            }

            var aiResponse = await GetHairStyleSuggestionFromGPT(inputDescription);

            ViewBag.Suggestion = aiResponse;
            return View();
        }

        private async Task<string> GetHairStyleSuggestionFromGPT(string inputDescription)
        {
            var client = _httpClientFactory.CreateClient();

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "You are an expert hair stylist AI that provides text-based suggestions." },
                    new { role = "user", content = inputDescription }
                }
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_OPENAI_API_KEY");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<dynamic>(responseContent);
                return responseObject?.choices[0]?.message?.content ?? "No suggestion available.";
            }
            else
            {
                return "Failed to fetch suggestion from AI.";
            }
        }
    }
}
