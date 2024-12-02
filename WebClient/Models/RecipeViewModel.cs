using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Creator is required.")]
        public string Creator { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Ingredients are required.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Instructions are required.")]
        public string Instructions { get; set; }
    }
}
