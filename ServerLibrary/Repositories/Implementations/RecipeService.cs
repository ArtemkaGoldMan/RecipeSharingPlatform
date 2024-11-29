using BaseLibrary.Entities;
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
        private readonly IRepository<Recipe> _repository;

        public RecipeService(IRepository<Recipe> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<Recipe> AddRecipeAsync(Recipe recipe)
        {
            return _repository.AddAsync(recipe);
        }

        public Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            return _repository.UpdateAsync(recipe);
        }

        public Task<bool> DeleteRecipeAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
