using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.DTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagDTO tagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tag = await _tagService.CreateTagAsync(tagDto);
            return CreatedAtAction(nameof(GetAllTags), tag);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var success = await _tagService.DeleteTagAsync(id);
            if (!success) return NotFound(new { message = "Tag not found" });
            return NoContent();
        }
    }
}
