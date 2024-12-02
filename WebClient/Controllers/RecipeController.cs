using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class RecipeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(HttpClient httpClient, ILogger<RecipeController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("https://localhost:7181/api/Recipe");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var recipes = JsonSerializer.Deserialize<List<RecipeViewModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return View(recipes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var token = HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7181/api/Recipe", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            // Handle errors
            ModelState.AddModelError(string.Empty, "An error occurred while creating the recipe.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"https://localhost:7181/api/Recipe/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var recipe = JsonSerializer.Deserialize<RecipeViewModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RecipeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var token = HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7181/api/Recipe/{id}", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update recipe");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"https://localhost:7181/api/Recipe/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7181/api/Recipe/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to fetch recipe details. Status Code: {response.StatusCode}");
                    return View("Error", new ErrorViewModel { RequestId = "API call failed." });
                }

                var json = await response.Content.ReadAsStringAsync();
                var recipe = JsonSerializer.Deserialize<RecipeViewModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(recipe);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

    }
}
