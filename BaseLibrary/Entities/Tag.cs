using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } 

        public ICollection<RecipeTag> RecipeTags { get; set; } // Navigation property for *:* relationship
    }
}
