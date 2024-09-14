using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Models
{
	public class Rating
	{
		public int RatingId { get; set; } // Primary Key

		[Required]
		[Range(1, 5)]
		public int Star { get; set; } // e.g., 1 to 5 stars

		// Foreign Keys
		public int UserId { get; set; }
		public User User { get; set; }

		public int RecipeId { get; set; }
		public Recipe Recipe { get; set; }
	}
}
