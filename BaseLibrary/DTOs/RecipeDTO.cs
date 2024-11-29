using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public List<int> IngredientIds { get; set; }
        public List<string> IngredientNames { get; set; }
    }
}
