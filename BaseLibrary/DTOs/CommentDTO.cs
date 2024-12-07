using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
    }
}
