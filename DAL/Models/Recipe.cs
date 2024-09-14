using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Recipe
    {
        [Key]
        [Required]
        public int RecipeId { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(50)]
        public string Cuisine { get; set; } // e.g., "Italian, Chinese"

      
        [StringLength(20)]
        public string Difficulty { get; set; } // e.g., "Easy, Medium, Hard"

        [Range(1, 1440)]
        public int CookingTime { get; set; } // in minutes

        // Foreign Key
        public int UserId { get; set; }
        //[ForeignKey("UserId")]
        //public User User { get; set; }


 
    }
}
