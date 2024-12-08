using BaseLibrary.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPFClient.Services
{
    public interface IRecipeService
    {
        // Recipe Management
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task AddRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int recipeId);

        // Details Management
        Task<RecipeDetails> GetDetailsByRecipeIdAsync(int recipeId);
        Task AddDetailsAsync(RecipeDetails details);
        Task UpdateDetailsAsync(RecipeDetails details);

        // Tags Management
        Task<IEnumerable<Tag>> GetTagsByRecipeIdAsync(int recipeId);
        Task AddTagAsync(Tag tag);
        Task DeleteTagAsync(int tagId);

        // Comments Management
        Task<IEnumerable<Comment>> GetCommentsByRecipeIdAsync(int recipeId);
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int commentId);
    }
}
