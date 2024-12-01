using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO categoryDto);
        Task<CategoryDTO> UpdateCategoryAsync(int id, CreateCategoryDTO categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
