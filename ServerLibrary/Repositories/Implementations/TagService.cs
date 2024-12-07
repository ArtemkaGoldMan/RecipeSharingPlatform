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
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            return await _context.Tags
                .Select(t => new TagDTO
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<TagDTO> CreateTagAsync(TagDTO tagDto)
        {
            var tag = new Tag
            {
                Name = tagDto.Name
            };

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            tagDto.Id = tag.Id; // Assign the generated ID to the DTO
            return tagDto;
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
