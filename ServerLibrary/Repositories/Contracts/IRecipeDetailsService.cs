using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IRecipeDetailsService
    {
        Task<RecipeDetailsDTO> GetDetailsByRecipeIdAsync(int recipeId);
        Task<RecipeDetailsDTO> CreateDetailsAsync(RecipeDetailsDTO recipeDetailsDto);
        Task<RecipeDetailsDTO> UpdateDetailsAsync(int id, RecipeDetailsDTO recipeDetailsDto);
        Task<bool> DeleteDetailsAsync(int id);
    }
}
