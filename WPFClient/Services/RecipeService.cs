using BaseLibrary.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService()
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri("https://localhost:7181/api/") };
        }

        // Recipe Management
        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            var response = await _httpClient.GetAsync("Recipe");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Recipe>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Recipe/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Recipe>(await response.Content.ReadAsStringAsync());
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            var response = await _httpClient.PostAsJsonAsync("Recipe", recipe);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var response = await _httpClient.PutAsJsonAsync($"Recipe/{recipe.Id}", recipe);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRecipeAsync(int recipeId)
        {
            var response = await _httpClient.DeleteAsync($"Recipe/{recipeId}");
            response.EnsureSuccessStatusCode();
        }

        // Details Management
        public async Task<RecipeDetails> GetDetailsByRecipeIdAsync(int recipeId)
        {
            var response = await _httpClient.GetAsync($"RecipeDetails/{recipeId}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<RecipeDetails>(await response.Content.ReadAsStringAsync());
        }

        public async Task AddDetailsAsync(RecipeDetails details)
        {
            var response = await _httpClient.PostAsJsonAsync("RecipeDetails", details);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDetailsAsync(RecipeDetails details)
        {
            var response = await _httpClient.PutAsJsonAsync($"RecipeDetails/{details.Id}", details);
            response.EnsureSuccessStatusCode();
        }

        // Tags Management
        public async Task<IEnumerable<Tag>> GetTagsByRecipeIdAsync(int recipeId)
        {
            var response = await _httpClient.GetAsync($"RecipeTag/{recipeId}/tags");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Tag>>(await response.Content.ReadAsStringAsync());
        }

        public async Task AddTagAsync(Tag tag)
        {
            var response = await _httpClient.PostAsJsonAsync("RecipeTag", tag);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var response = await _httpClient.DeleteAsync($"RecipeTag/{tagId}");
            response.EnsureSuccessStatusCode();
        }

        // Comments Management
        public async Task<IEnumerable<Comment>> GetCommentsByRecipeIdAsync(int recipeId)
        {
            var response = await _httpClient.GetAsync($"Comment/{recipeId}/comments");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Comment>>(await response.Content.ReadAsStringAsync());
        }

        public async Task AddCommentAsync(Comment comment)
        {
            var response = await _httpClient.PostAsJsonAsync("Comment", comment);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            var response = await _httpClient.PutAsJsonAsync($"Comment/{comment.Id}", comment);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var response = await _httpClient.DeleteAsync($"Comment/{commentId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
