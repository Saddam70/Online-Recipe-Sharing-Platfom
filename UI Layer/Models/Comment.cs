using System.ComponentModel.DataAnnotations;
using System;

namespace UI_Layer.Models
{
	public class Comment
	{
		public int CommentId { get; set; } // Primary Key

		

		public string Content { get; set; }

		[DataType(DataType.Date)]
		public DateTime CommentDate { get; set; }

		// Foreign Keys
		public int UserId { get; set; }
		public User User { get; set; }

		public int RecipeId { get; set; }
		public Recipe Recipe { get; set; }
	}
}
