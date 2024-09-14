using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Models
{
	public class RecipeStep
	{
		public int RecipeStepId { get; set; } // Primary Key

		
		public int StepNumber { get; set; }

	
		public string Instruction { get; set; }

		public string ImageUrl { get; set; } // URL for the step image

		// Foreign Key
		
		public int RecipeId { get; set; }
		public Recipe Recipe { get; set; }
	}
}
