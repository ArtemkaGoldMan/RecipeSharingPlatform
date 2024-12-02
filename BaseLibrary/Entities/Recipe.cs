using System.ComponentModel;

namespace BaseLibrary.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; } // The name of the person who created the recipe
        public string Category { get; set; } // Example: "Dessert"
        public string Ingredients { get; set; } // Comma-separated list of ingredients
        public string Instructions { get; set; } // Detailed instructions
    }

}