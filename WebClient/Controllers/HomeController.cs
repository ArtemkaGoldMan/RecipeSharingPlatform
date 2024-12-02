using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Call the API
                var response = await _httpClient.GetAsync("https://localhost:7181/api/Recipe");

                // Ensure a successful response
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to fetch recipes. Status Code: {response.StatusCode}");
                    return View(new List<RecipeViewModel>());
                }

                // Deserialize the response
                var json = await response.Content.ReadAsStringAsync();
                var recipes = JsonSerializer.Deserialize<List<RecipeViewModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Pass the recipes to the view
                return View(recipes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching recipes: {ex.Message}");
                return View(new List<RecipeViewModel>());
            }
        }
    }
}
