using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class RecipesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(HttpClient httpClient, ILogger<RecipesController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Display all recipes
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7181/api/Recipe");
            if (!response.IsSuccessStatusCode) return View("Error");

            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeDTO>>();
            return View(recipes);
        }

        // Display Recipe Details, including RecipeDetails, Tags, and Comments
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                // Fetch Recipe
                var recipeResponse = await _httpClient.GetAsync($"https://localhost:7181/api/Recipe/{id}");
                if (!recipeResponse.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to fetch recipe: {recipeResponse.StatusCode}");
                    return View("Error");
                }

                var recipe = await recipeResponse.Content.ReadFromJsonAsync<RecipeDTO>();

                // Fetch RecipeDetails
                var detailsResponse = await _httpClient.GetAsync($"https://localhost:7181/api/RecipeDetails/{id}");
                RecipeDetailsDTO recipeDetails = null;
                if (detailsResponse.IsSuccessStatusCode)
                {
                    recipeDetails = await detailsResponse.Content.ReadFromJsonAsync<RecipeDetailsDTO>();
                }
                else
                {
                    _logger.LogInformation($"No details found for RecipeId: {id}");
                }

                // Fetch Tags
                var tagsResponse = await _httpClient.GetAsync($"https://localhost:7181/api/RecipeTag/{id}/tags");
                List<TagDTO> tags = new List<TagDTO>();
                if (tagsResponse.IsSuccessStatusCode)
                {
                    tags = await tagsResponse.Content.ReadFromJsonAsync<List<TagDTO>>();
                }
                else
                {
                    _logger.LogInformation($"No tags found for RecipeId: {id}");
                }

                // Fetch Comments
                var commentsResponse = await _httpClient.GetAsync($"https://localhost:7181/api/Comment/{id}/comments");
                List<CommentDTO> comments = new List<CommentDTO>();
                if (commentsResponse.IsSuccessStatusCode)
                {
                    comments = await commentsResponse.Content.ReadFromJsonAsync<List<CommentDTO>>();
                }
                else
                {
                    _logger.LogInformation($"No comments found for RecipeId: {id}");
                }

                // Create ViewModel
                var viewModel = new RecipeViewModel
                {
                    Recipe = recipe,
                    RecipeDetails = recipeDetails,
                    Tags = tags,
                    Comments = comments
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching recipe details.");
                return View("Error");
            }
        }

        // Display the Create page
        public IActionResult Create()
        {
            var viewModel = new RecipeViewModel
            {
                Recipe = new RecipeDTO(),
                RecipeDetails = new RecipeDetailsDTO(),
                Tags = new List<TagDTO>(),
                Comments = new List<CommentDTO>()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeViewModel viewModel)
        {
            try
            {
                // Create the Recipe
                var recipeResponse = await _httpClient.PostAsJsonAsync("https://localhost:7181/api/Recipe", viewModel.Recipe);
                if (!recipeResponse.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to create recipe: {recipeResponse.StatusCode}");
                    return View("Error");
                }

                // Extract the created RecipeId
                var createdRecipe = await recipeResponse.Content.ReadFromJsonAsync<RecipeDTO>();
                var recipeId = createdRecipe.Id;

                // Create RecipeDetails
                viewModel.RecipeDetails.RecipeId = recipeId;
                var detailsResponse = await _httpClient.PostAsJsonAsync("https://localhost:7181/api/RecipeDetails", viewModel.RecipeDetails);
                if (!detailsResponse.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to create recipe details: {detailsResponse.StatusCode}");
                    return View("Error");
                }

                // Create Tags
                foreach (var tag in viewModel.Tags)
                {
                    tag.Id = recipeId;
                    var tagResponse = await _httpClient.PostAsJsonAsync("https://localhost:7181/api/RecipeTag", tag);
                    if (!tagResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Failed to create tag: {tagResponse.StatusCode}");
                    }
                }

                // Create Comments
                foreach (var comment in viewModel.Comments)
                {
                    comment.RecipeId = recipeId;
                    var commentResponse = await _httpClient.PostAsJsonAsync("https://localhost:7181/api/Comment", comment);
                    if (!commentResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Failed to create comment: {commentResponse.StatusCode}");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a recipe.");
                return View("Error");
            }
        }

        // Edit Recipe (updates Recipe, Details, Tags, Comments)
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Fetch Recipe
                var recipeResponse = await _httpClient.GetAsync($"https://localhost:7181/api/Recipe/{id}");
                if (!recipeResponse.IsSuccessStatusCode) return View("Error");

                var recipe = await recipeResponse.Content.ReadFromJsonAsync<RecipeDTO>();

                // Fetch RecipeDetails
                var detailsResponse = await _httpClient.GetAsync($"https://localhost:7181/api/RecipeDetails/{id}");
                var recipeDetails = detailsResponse.IsSuccessStatusCode
                    ? await detailsResponse.Content.ReadFromJsonAsync<RecipeDetailsDTO>()
                    : new RecipeDetailsDTO { RecipeId = id };

                // Fetch Tags
                var tagsResponse = await _httpClient.GetAsync($"https://localhost:7181/api/RecipeTag/{id}/tags");
                var tags = tagsResponse.IsSuccessStatusCode
                    ? await tagsResponse.Content.ReadFromJsonAsync<List<TagDTO>>()
                    : new List<TagDTO>();

                // Fetch Comments
                var commentsResponse = await _httpClient.GetAsync($"https://localhost:7181/api/Comment/{id}/comments");
                var comments = commentsResponse.IsSuccessStatusCode
                    ? await commentsResponse.Content.ReadFromJsonAsync<List<CommentDTO>>()
                    : new List<CommentDTO>();

                var viewModel = new RecipeViewModel
                {
                    Recipe = recipe,
                    RecipeDetails = recipeDetails,
                    Tags = tags,
                    Comments = comments
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching recipe details for editing.");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecipeViewModel viewModel)
        {
            try
            {
                // Log Recipe data
                _logger.LogInformation($"Updating Recipe: {System.Text.Json.JsonSerializer.Serialize(viewModel.Recipe)}");

                // Update Recipe
                var recipeResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7181/api/Recipe/{viewModel.Recipe.Id}", viewModel.Recipe);
                if (!recipeResponse.IsSuccessStatusCode)
                {
                    var error = await recipeResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to update recipe: {recipeResponse.StatusCode}. Error: {error}");
                    return View("Error");
                }

                // Log RecipeDetails data
                _logger.LogInformation($"Updating RecipeDetails: {System.Text.Json.JsonSerializer.Serialize(viewModel.RecipeDetails)}");

                // Update RecipeDetails
                var detailsResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7181/api/RecipeDetails/{viewModel.RecipeDetails.Id}", viewModel.RecipeDetails);
                if (!detailsResponse.IsSuccessStatusCode)
                {
                    var error = await detailsResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to update recipe details: {detailsResponse.StatusCode}. Error: {error}");
                }

                // Update Tags
                foreach (var tag in viewModel.Tags)
                {
                    _logger.LogInformation($"Updating Tag: {System.Text.Json.JsonSerializer.Serialize(tag)}");
                    var tagResponse = tag.Id == 0
                        ? await _httpClient.PostAsJsonAsync("https://localhost:7181/api/RecipeTag", tag)
                        : await _httpClient.PutAsJsonAsync($"https://localhost:7181/api/RecipeTag/{tag.Id}", tag);

                    if (!tagResponse.IsSuccessStatusCode)
                    {
                        var error = await tagResponse.Content.ReadAsStringAsync();
                        _logger.LogError($"Failed to update tag: {tagResponse.StatusCode}. Error: {error}");
                    }
                }

                // Update Comments
                foreach (var comment in viewModel.Comments)
                {
                    _logger.LogInformation($"Updating Comment: {System.Text.Json.JsonSerializer.Serialize(comment)}");
                    var commentResponse = comment.Id == 0
                        ? await _httpClient.PostAsJsonAsync("https://localhost:7181/api/Comment", comment)
                        : await _httpClient.PutAsJsonAsync($"https://localhost:7181/api/Comment/{comment.Id}", comment);

                    if (!commentResponse.IsSuccessStatusCode)
                    {
                        var error = await commentResponse.Content.ReadAsStringAsync();
                        _logger.LogError($"Failed to update comment: {commentResponse.StatusCode}. Error: {error}");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the recipe.");
                return View("Error");
            }
        }



        // Delete Recipe (and associated Details, Tags, Comments)
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7181/api/Recipe/{id}");
            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction(nameof(Index));
        }
    }
}
