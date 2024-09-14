using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Models
{
	public class Ingredient
	{
		public int IngredientId { get; set; } // Primary Key

		
		public string Name { get; set; } // e.g., "Tomato, Olive Oil"

		
		public string Quantity { get; set; } // e.g., "2 cups, 1 tablespoon"

		// Foreign Key
		public int RecipeId { get; set; }
		public Recipe Recipe { get; set; }
	}
}
