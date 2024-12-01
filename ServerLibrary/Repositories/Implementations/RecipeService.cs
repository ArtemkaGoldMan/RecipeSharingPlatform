using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Recipe> CreateRecipeAsync(RecipeDTO recipeDto)
        {
            var recipe = new Recipe
            {
                Title = recipeDto.Title,
                Description = recipeDto.Description,
                CategoryId = recipeDto.CategoryId,
                UserId = recipeDto.UserId, // Associate the recipe with the user
                RecipeIngredients = recipeDto.IngredientIds.Select(id => new RecipeIngredient
                {
                    IngredientId = id
                }).ToList()
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }


        public async Task<Recipe> UpdateRecipeAsync(int id, RecipeDTO recipeDto)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return null;

            recipe.Title = recipeDto.Title;
            recipe.Description = recipeDto.Description;
            recipe.CategoryId = recipeDto.CategoryId;

            recipe.RecipeIngredients = recipeDto.IngredientIds.Select(id => new RecipeIngredient
            {
                RecipeId = id,
                IngredientId = id
            }).ToList();

            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
