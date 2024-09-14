using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace UI_Layer.Models
{
	public class User
	{

		
		public int UserId { get; set; } // Primary Key

		
		public string Username { get; set; }
		
		public string EmailId { get; set; }


		public string Password { get; set; }

		[RegularExpression("^(Admin|User|Customer)$", ErrorMessage = "Role Must be Matched")]
		[StringLength(maximumLength: 10)]
		[DefaultValue("User")]
		public string Role { get; set; }
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		[Required]
		public string Gender { get; set; }
		public string FavoriteCuisines { get; set; } // e.g., "Italian, Chinese"
		public string DietaryPreferences { get; set; } // e.g., "Vegetarian, Vegan"
	}
}
