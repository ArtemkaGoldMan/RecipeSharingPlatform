using System.ComponentModel;

namespace BaseLibrary.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; } // Link to the creator (User)
        public User User { get; set; }

        // Flattening other entities into simple fields
        public string Category { get; set; } // Example: "Dessert"
        public string Ingredients { get; set; } // Example: "Flour, Sugar, Eggs"
        public string Instructions { get; set; } // Example: Step-by-step instructions as text
    }

}