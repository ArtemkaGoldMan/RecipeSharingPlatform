using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class RecipeTagService : IRecipeTagService
    {
        private readonly AppDbContext _context;

        public RecipeTagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagDTO>> GetTagsForRecipeAsync(int recipeId)
        {
            return await _context.RecipeTags
                .Where(rt => rt.RecipeId == recipeId)
                .Include(rt => rt.Tag)
                .Select(rt => new TagDTO
                {
                    Id = rt.TagId,
                    Name = rt.Tag.Name
                })
                .ToListAsync();
        }

        public async Task AddTagToRecipeAsync(RecipeTagDTO recipeTagDto)
        {
            // Check if the relationship already exists
            var existingRelation = await _context.RecipeTags
                .FirstOrDefaultAsync(rt => rt.RecipeId == recipeTagDto.RecipeId && rt.TagId == recipeTagDto.TagId);

            if (existingRelation == null)
            {
                var recipeTag = new RecipeTag
                {
                    RecipeId = recipeTagDto.RecipeId,
                    TagId = recipeTagDto.TagId
                };

                _context.RecipeTags.Add(recipeTag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveTagFromRecipeAsync(RecipeTagDTO recipeTagDto)
        {
            var recipeTag = await _context.RecipeTags
                .FirstOrDefaultAsync(rt => rt.RecipeId == recipeTagDto.RecipeId && rt.TagId == recipeTagDto.TagId);

            if (recipeTag != null)
            {
                _context.RecipeTags.Remove(recipeTag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
