using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.DTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{recipeId}/comments")]
        public async Task<IActionResult> GetCommentsForRecipe(int recipeId)
        {
            var comments = await _commentService.GetCommentsByRecipeIdAsync(recipeId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentService.CreateCommentAsync(commentDto);
            return CreatedAtAction(nameof(GetCommentsForRecipe), new { recipeId = comment.RecipeId }, comment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDTO commentDto)
        {
            var updatedComment = await _commentService.UpdateCommentAsync(id, commentDto);
            if (updatedComment == null) return NotFound(new { message = "Recipe not found" });
            return Ok(updatedComment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var success = await _commentService.DeleteCommentAsync(id);
            if (!success) return NotFound(new { message = "Comment not found" });
            return NoContent();
        }
    }
}
