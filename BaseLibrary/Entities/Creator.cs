namespace BaseLibrary.Entities
{
    public class Creator
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // Foreign key to User
        public ICollection<Recipe> Recipes { get; set; }
    }

}