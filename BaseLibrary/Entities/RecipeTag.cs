using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class RecipeTag
    {
        public int RecipeId { get; set; } // Foreign key
        public Recipe Recipe { get; set; } // Navigation property
        public int TagId { get; set; } // Foreign key
        public Tag Tag { get; set; } // Navigation property
    }
}
