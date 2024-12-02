using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService()
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri("https://localhost:7181/api/") };
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Recipe");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"API Response: {json}");

                return JsonConvert.DeserializeObject<IEnumerable<Recipe>>(json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAllRecipesAsync: {ex.Message}");
                return Enumerable.Empty<Recipe>();
            }
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            var response = await _httpClient.PostAsJsonAsync("Recipe", recipe); // Remove the redundant "api/"
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error adding recipe: {response.StatusCode} - {error}");
            }
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var json = JsonConvert.SerializeObject(recipe);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Recipe/{recipe.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRecipeAsync(int recipeId)
        {
            var response = await _httpClient.DeleteAsync($"Recipe/{recipeId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
