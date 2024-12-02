using System;
using System.Collections.Generic;

namespace BaseLibrary.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } // For returning the user's name
        public string Category { get; set; } // Example: "Dessert"
        public string Ingredients { get; set; } // Example: "Flour, Sugar, Eggs"
        public string Instructions { get; set; } // Detailed instructions
    }

}
