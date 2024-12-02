using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.DTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null) return NotFound(new { message = "Recipe not found" });
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeDTO recipeDto)
        {
            var newRecipe = await _recipeService.CreateRecipeAsync(recipeDto);
            return CreatedAtAction(nameof(GetRecipeById), new { id = newRecipe.Id }, newRecipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeDTO recipeDto)
        {
            var updatedRecipe = await _recipeService.UpdateRecipeAsync(id, recipeDto);
            if (updatedRecipe == null) return NotFound(new { message = "Recipe not found" });
            return Ok(updatedRecipe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var success = await _recipeService.DeleteRecipeAsync(id);
            if (!success) return NotFound(new { message = "Recipe not found" });
            return Ok(new { message = "Recipe deleted successfully" });
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _recipeService.GetAllCategoriesAsync();
            return Ok(categories);
        }
    }
}
