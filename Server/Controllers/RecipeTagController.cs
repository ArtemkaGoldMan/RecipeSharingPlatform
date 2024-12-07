using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.DTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeTagController : ControllerBase
    {
        private readonly IRecipeTagService _recipeTagService;

        public RecipeTagController(IRecipeTagService recipeTagService)
        {
            _recipeTagService = recipeTagService;
        }

        [HttpGet("{recipeId}/tags")]
        public async Task<IActionResult> GetTagsForRecipe(int recipeId)
        {
            var tags = await _recipeTagService.GetTagsForRecipeAsync(recipeId);
            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> AddTagToRecipe([FromBody] RecipeTagDTO recipeTagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _recipeTagService.AddTagToRecipeAsync(recipeTagDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTagFromRecipe([FromBody] RecipeTagDTO recipeTagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _recipeTagService.RemoveTagFromRecipeAsync(recipeTagDto);
            return NoContent();
        }
    }
}
