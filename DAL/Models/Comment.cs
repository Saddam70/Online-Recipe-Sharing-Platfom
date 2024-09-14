using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; } // Primary Key

        [Required]
        
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CommentDate { get; set; }

        // Foreign Keys
        public int UserId {  get; set; }
       //public User User { get; set; }

        public int RecipeId { get; set; }
        //public Recipe Recipe { get; set; }
    }
}
