using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class RecipeDetailsService : IRecipeDetailsService
    {
        private readonly AppDbContext _context;

        public RecipeDetailsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RecipeDetailsDTO> GetDetailsByRecipeIdAsync(int recipeId)
        {
            var details = await _context.RecipeDetails.FirstOrDefaultAsync(rd => rd.RecipeId == recipeId);
            if (details == null) return null;

            return new RecipeDetailsDTO
            {
                Id = details.Id,
                RecipeId = details.RecipeId,
                NutritionInfo = details.NutritionInfo,
                PreparationTime = details.PreparationTime
            };
        }

        public async Task<RecipeDetailsDTO> CreateDetailsAsync(RecipeDetailsDTO recipeDetailsDto)
        {
            var details = new RecipeDetails
            {
                RecipeId = recipeDetailsDto.RecipeId,
                NutritionInfo = recipeDetailsDto.NutritionInfo,
                PreparationTime = recipeDetailsDto.PreparationTime
            };

            _context.RecipeDetails.Add(details);
            await _context.SaveChangesAsync();

            return recipeDetailsDto;
        }

        public async Task<RecipeDetailsDTO> UpdateDetailsAsync(int id, RecipeDetailsDTO recipeDetailsDto)
        {
            var details = await _context.RecipeDetails.FindAsync(id);
            if (details == null) return null;

            details.NutritionInfo = recipeDetailsDto.NutritionInfo;
            details.PreparationTime = recipeDetailsDto.PreparationTime;

            await _context.SaveChangesAsync();

            return recipeDetailsDto;
        }

        public async Task<bool> DeleteDetailsAsync(int id)
        {
            var details = await _context.RecipeDetails.FindAsync(id);
            if (details == null) return false;

            _context.RecipeDetails.Remove(details);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
