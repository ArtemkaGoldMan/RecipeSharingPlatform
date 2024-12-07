using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeDetailsController : ControllerBase
    {
        private readonly IRecipeDetailsService _recipeDetailsService;

        public RecipeDetailsController(IRecipeDetailsService recipeDetailsService)
        {
            _recipeDetailsService = recipeDetailsService;
        }

        // Get details by Recipe ID
        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetDetailsByRecipeId(int recipeId)
        {
            var details = await _recipeDetailsService.GetDetailsByRecipeIdAsync(recipeId);
            if (details == null)
                return NotFound(new { message = "Details not found for the given recipe ID" });

            return Ok(details);
        }

        // Create details
        [HttpPost]
        public async Task<IActionResult> CreateDetails([FromBody] RecipeDetailsDTO recipeDetailsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDetails = await _recipeDetailsService.CreateDetailsAsync(recipeDetailsDto);
            return CreatedAtAction(nameof(GetDetailsByRecipeId), new { recipeId = createdDetails.RecipeId }, createdDetails);
        }

        // Update details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetails(int id, [FromBody] RecipeDetailsDTO recipeDetailsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedDetails = await _recipeDetailsService.UpdateDetailsAsync(id, recipeDetailsDto);
            if (updatedDetails == null)
                return NotFound(new { message = "Details not found to update" });

            return Ok(updatedDetails);
        }

        // Delete details
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetails(int id)
        {
            var success = await _recipeDetailsService.DeleteDetailsAsync(id);
            if (!success)
                return NotFound(new { message = "Details not found to delete" });

            return NoContent();
        }
    }
}
