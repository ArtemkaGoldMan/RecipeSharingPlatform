using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetCommentsByRecipeIdAsync(int recipeId);
        Task<CommentDTO> CreateCommentAsync(CommentDTO commentDto);
        Task<CommentDTO> UpdateCommentAsync(int id, CommentDTO commentDto);
        Task<bool> DeleteCommentAsync(int id);
    }
}
