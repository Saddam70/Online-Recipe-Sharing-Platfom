using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    
    public class Admin
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 100)]
        public string EmailId { get; set; }

      
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        [Required]
        public string Password { get; set; }

        //[RegularExpression("^(Admin|User|Customer)$", ErrorMessage = "Role Not Matched")]
        [StringLength(maximumLength: 10)]
        [DefaultValue("Admin")]
        public string Role { get; set; }
    }
}
