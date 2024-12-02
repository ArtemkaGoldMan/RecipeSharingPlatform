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
            var recipes = await _context.Recipes.Include(r => r.User).ToListAsync();

            return recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                Username = r.User?.Username, // Include only user name, not the entire User object
                Category = r.Category,
                Ingredients = r.Ingredients,
                Instructions = r.Instructions
            }).ToList();
        }


        // Get a single recipe by ID
        public async Task<RecipeDTO> GetRecipeByIdAsync(int id)
        {
            var recipe = await _context.Recipes.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return null;

            return new RecipeDTO
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Username = recipe.User?.Username,
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
                UserId = recipeDto.UserId, // Associate with the user
                Category = recipeDto.Category, // Store the category as a string
                Ingredients = recipeDto.Ingredients, // Use the single string for ingredients
                Instructions = recipeDto.Instructions // Store instructions
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
            recipe.Category = recipeDto.Category;
            recipe.Ingredients = recipeDto.Ingredients; // Use the Ingredients string directly
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

        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _context.Recipes
                .Select(r => r.Category)
                .Distinct()
                .ToListAsync();
        }

    }
}
