using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class RecipeDetailsDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string NutritionInfo { get; set; }
        public string PreparationTime { get; set; }
    }
}
