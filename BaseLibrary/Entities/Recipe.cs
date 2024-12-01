using System.ComponentModel;

namespace BaseLibrary.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; } // Link directly to User
        public User User { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}