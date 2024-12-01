using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly AppDbContext _context;

        public IngredientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IngredientDTO>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients
                .Select(i => new IngredientDTO
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .ToListAsync();
        }

        public async Task<IngredientDTO> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null) return null;

            return new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
        }

        public async Task<IngredientDTO> CreateIngredientAsync(CreateIngredientDTO ingredientDto)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientDto.Name
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
        }

        public async Task<IngredientDTO> UpdateIngredientAsync(int id, CreateIngredientDTO ingredientDto)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null) return null;

            ingredient.Name = ingredientDto.Name;
            await _context.SaveChangesAsync();

            return new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
        }

        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null) return false;

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
