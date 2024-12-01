using System;
using System.Collections.Generic;

namespace BaseLibrary.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int UserId { get; set; } // Link to User instead of Creator
        public string Username { get; set; } // User's name instead of Creator's name
        public List<int> IngredientIds { get; set; } // List of ingredient IDs
        public List<string> IngredientNames { get; set; } // List of ingredient names
    }
}
