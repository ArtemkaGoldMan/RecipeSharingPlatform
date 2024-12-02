using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDTO>> GetAllRecipesAsync();
        Task<RecipeDTO> GetRecipeByIdAsync(int id);
        Task<Recipe> CreateRecipeAsync(RecipeDTO recipeDto);
        Task<Recipe> UpdateRecipeAsync(int id, RecipeDTO recipeDto);
        Task<bool> DeleteRecipeAsync(int id);
        Task<IEnumerable<string>> GetAllCategoriesAsync();
    }
}
