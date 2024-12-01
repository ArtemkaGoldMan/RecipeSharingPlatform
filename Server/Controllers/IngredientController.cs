using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.Entities;
using BaseLibrary.DTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null) return NotFound(new { message = "Ingredient not found" });
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDTO ingredientDto)
        {
            var newIngredient = await _ingredientService.CreateIngredientAsync(ingredientDto);
            return CreatedAtAction(nameof(GetIngredientById), new { id = newIngredient.Id }, newIngredient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] CreateIngredientDTO ingredientDto)
        {
            var updatedIngredient = await _ingredientService.UpdateIngredientAsync(id, ingredientDto);
            if (updatedIngredient == null) return NotFound(new { message = "Ingredient not found" });
            return Ok(updatedIngredient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var success = await _ingredientService.DeleteIngredientAsync(id);
            if (!success) return NotFound(new { message = "Ingredient not found" });
            return Ok(new { message = "Ingredient deleted successfully" });
        }
    }
}
