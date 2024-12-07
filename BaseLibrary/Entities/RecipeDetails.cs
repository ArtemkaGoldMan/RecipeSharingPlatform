using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class RecipeDetails
    {
        public int Id { get; set; }
        public int RecipeId { get; set; } // Foreign key
        public string NutritionInfo { get; set; } // Example: "Calories: 200, Protein: 5g"
        public string PreparationTime { get; set; } // Example: "20 minutes"

        public Recipe Recipe { get; set; } // Navigation property
    }

}
