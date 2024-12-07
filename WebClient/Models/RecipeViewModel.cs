using BaseLibrary.DTOs;
using System.Collections.Generic;

namespace WebClient.Models
{
    public class RecipeViewModel
    {
        public RecipeDTO Recipe { get; set; }
        public RecipeDetailsDTO RecipeDetails { get; set; }
        public List<TagDTO> Tags { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
