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
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByRecipeIdAsync(int recipeId)
        {
            return await _context.Comments
                .Where(c => c.RecipeId == recipeId)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    RecipeId = c.RecipeId,
                    Text = c.Text,
                    Author = c.Author
                })
                .ToListAsync();
        }

        public async Task<CommentDTO> CreateCommentAsync(CommentDTO commentDto)
        {
            var comment = new Comment
            {
                RecipeId = commentDto.RecipeId,
                Text = commentDto.Text,
                Author = commentDto.Author
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return commentDto;
        }

        public async Task<CommentDTO> UpdateCommentAsync(int id, CommentDTO commentDto)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return null;

            comment.Text = commentDto.Text;
            comment.Author = commentDto.Author;

            await _context.SaveChangesAsync();
            return commentDto;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
