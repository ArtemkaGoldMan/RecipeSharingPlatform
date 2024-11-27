namespace BaseLibrary.Entities
{
    public class Creator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}