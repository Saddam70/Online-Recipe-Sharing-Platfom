using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RecipeStep
    {
        [Key]
        public int RecipeStepId { get; set; } // Primary Key

        [Required]
        public int StepNumber { get; set; }

        [Required]
        public string Instruction { get; set; }

        public string ImageUrl { get; set; } // URL for the step image

        // Foreign Key
        [Required]
        public int RecipeId { get; set; }
       // public Recipe Recipe { get; set; }
    }
}
