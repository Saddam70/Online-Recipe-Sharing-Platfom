using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UI_Layer.Models
{
	public class Users
	{
		public string EmailId { get; set; }


		public string Password { get; set; }

		[RegularExpression("^(Admin|User|Customer)$", ErrorMessage = "Role Must be Matched")]
		[StringLength(maximumLength: 10)]
		[DefaultValue("User")]
		public string Role { get; set; }
	}
}
