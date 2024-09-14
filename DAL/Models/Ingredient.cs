using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // e.g., "Tomato, Olive Oil"

        [Required]
        [StringLength(50)]
        public string Quantity { get; set; } // e.g., "2 cups, 1 tablespoon"

        // Foreign Key
        public int RecipeId { get; set; }
       // public Recipe Recipe { get; set; }
    }
}
