using System.ComponentModel;

namespace BaseLibrary
{
    public class Recipe{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CreatorId { get; set; }
        public Creator Creator { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set;}
    }
}