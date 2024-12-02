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
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        // Get all recipes
        public async Task<IEnumerable<RecipeDTO>> GetAllRecipesAsync()
        {
            var recipes = await _context.Recipes.ToListAsync();

            return recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                Creator = r.Creator, // Use the Creator property instead of User
                Category = r.Category,
                Ingredients = r.Ingredients,
                Instructions = r.Instructions
            }).ToList();
        }

        // Get a single recipe by ID
        public async Task<RecipeDTO> GetRecipeByIdAsync(int id)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return null;

            return new RecipeDTO
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Creator = recipe.Creator,
                Category = recipe.Category,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions
            };
        }

        // Create a new recipe
        public async Task<Recipe> CreateRecipeAsync(RecipeDTO recipeDto)
        {
            var recipe = new Recipe
            {
                Title = recipeDto.Title,
                Description = recipeDto.Description,
                Creator = recipeDto.Creator, // Set the Creator directly
                Category = recipeDto.Category,
                Ingredients = recipeDto.Ingredients,
                Instructions = recipeDto.Instructions
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        // Update an existing recipe
        public async Task<Recipe> UpdateRecipeAsync(int id, RecipeDTO recipeDto)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return null;

            recipe.Title = recipeDto.Title;
            recipe.Description = recipeDto.Description;
            recipe.Creator = recipeDto.Creator;
            recipe.Category = recipeDto.Category;
            recipe.Ingredients = recipeDto.Ingredients;
            recipe.Instructions = recipeDto.Instructions;

            await _context.SaveChangesAsync();
            return recipe;
        }

        // Delete a recipe by ID
        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all categories
        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _context.Recipes
                .Select(r => r.Category)
                .Distinct()
                .ToListAsync();
        }
    }
}
