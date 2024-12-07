using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IRecipeTagService
    {
        Task<IEnumerable<TagDTO>> GetTagsForRecipeAsync(int recipeId);
        Task AddTagToRecipeAsync(RecipeTagDTO recipeTagDto);
        Task RemoveTagFromRecipeAsync(RecipeTagDTO recipeTagDto);
    }
}
