using System;
using System.Collections.Generic;

namespace BaseLibrary.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; } // The name of the recipe creator
        public string Category { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
    }

}
