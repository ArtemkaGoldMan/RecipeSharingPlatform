using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class RecipesController : Controller
    {
        private readonly HttpClient _httpClient;

        public RecipesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Display all recipes
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7181/api/Recipe");
            if (!response.IsSuccessStatusCode) return View("Error");

            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeViewModel>>();
            return View(recipes);
        }

        // Display the Create page
        public IActionResult Create()
        {
            return View(new RecipeViewModel());
        }

        // Handle recipe creation
        [HttpPost]
        public async Task<IActionResult> Create(RecipeViewModel recipe)
        {
            if (!ModelState.IsValid) return View(recipe);

            var content = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7181/api/Recipe", content);

            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction(nameof(Index));
        }

        // Display the Edit page
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7181/api/Recipe/{id}");
            if (!response.IsSuccessStatusCode) return View("Error");

            var recipe = await response.Content.ReadFromJsonAsync<RecipeViewModel>();
            return View(recipe);
        }

        // Handle recipe editing
        [HttpPost]
        public async Task<IActionResult> Edit(RecipeViewModel recipe)
        {
            if (!ModelState.IsValid) return View(recipe);

            var content = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7181/api/Recipe/{recipe.Id}", content);

            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction(nameof(Index));
        }

        // Delete a recipe by ID
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7181/api/Recipe/{id}");
            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction(nameof(Index));
        }

        // Display the Details page
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7181/api/Recipe/{id}");
            if (!response.IsSuccessStatusCode) return View("Error");

            var recipe = await response.Content.ReadFromJsonAsync<RecipeViewModel>();
            return View(recipe);
        }
    }
}
