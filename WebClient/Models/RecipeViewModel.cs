namespace WebClient.Models
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
    }
}
