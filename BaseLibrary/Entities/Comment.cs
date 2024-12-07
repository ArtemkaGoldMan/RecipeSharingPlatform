using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int RecipeId { get; set; } // Foreign key
        public string Text { get; set; } // Comment text
        public string Author { get; set; } // Name of the person who commented
        public Recipe Recipe { get; set; } // Navigation property
    }
}
