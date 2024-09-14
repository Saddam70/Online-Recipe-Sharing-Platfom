using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; } // Primary Key

        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required]
        [StringLength (maximumLength:20,MinimumLength =6)]
        public string Password { get; set; }

        [RegularExpression("^(Admin|User)$",ErrorMessage ="Role Must be Matched")]
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

