using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface ITagService
    {
        Task<IEnumerable<TagDTO>> GetAllTagsAsync();
        Task<TagDTO> CreateTagAsync(TagDTO tagDto);
        Task<bool> DeleteTagAsync(int id);
    }
}
