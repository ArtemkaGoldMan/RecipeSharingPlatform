using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDTO>> GetAllIngredientsAsync();
        Task<IngredientDTO> GetIngredientByIdAsync(int id);
        Task<IngredientDTO> CreateIngredientAsync(CreateIngredientDTO ingredientDto);
        Task<IngredientDTO> UpdateIngredientAsync(int id, CreateIngredientDTO ingredientDto);
        Task<bool> DeleteIngredientAsync(int id);
    }
}
