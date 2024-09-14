using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Models
{
	public class Recipe
	{
		
		public int RecipeId { get; set; } // Primary Key

		
		public string Title { get; set; }

		
		public string Description { get; set; }

		
		public string Cuisine { get; set; } // e.g., "Italian, Chinese"
		
	
		public string Difficulty { get; set; } // e.g., "Easy, Medium, Hard"
       public int CookingTime { get; set; } // in minutes

		// Foreign Key
		public int UserId { get; set; }
		//[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
