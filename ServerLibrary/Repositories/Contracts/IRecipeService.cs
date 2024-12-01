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
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<Recipe> CreateRecipeAsync(RecipeDTO recipeDto);
        Task<Recipe> UpdateRecipeAsync(int id, RecipeDTO recipeDto);
        Task<bool> DeleteRecipeAsync(int id);
    }
}
