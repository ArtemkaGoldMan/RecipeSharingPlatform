using System.ComponentModel;
using System.Xml.Linq;

namespace BaseLibrary.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; } 
        public string Category { get; set; } 
        public string Ingredients { get; set; } 
        public string Instructions { get; set; } 

        // Relationships
        public RecipeDetails RecipeDetails { get; set; } // 1:1 relationship
        public ICollection<Comment> Comments { get; set; } // 1:* relationship
        public ICollection<RecipeTag> RecipeTags { get; set; } // *:* relationship
    }

}