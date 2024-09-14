using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CommentRepo : ICommentRepo<Comment>
    {
		//public Comment AddComment(Comment comment);
		//public Comment DeleteComment(int id);
		//public Comment GetCommentById(int id);
		//public IEnumerable<Comment> GetAllComment();
		//public Comment UpdateComment(Comment comment);
		public Comment AddComment(Comment comment)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();
                return comment;
            }
        }

        public Comment DeleteComment(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var comment = dbContext.Comments.FirstOrDefault(c => c.CommentId == id);
                if (comment != null)
                {
                    dbContext.Comments.Remove(comment);
                    dbContext.SaveChanges();
                }
                return comment;
            }
        }

        public Comment GetCommentById(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                return dbContext.Comments.FirstOrDefault(c => c.CommentId == id);
            }
        }
        public List<Comment> GetAllComment()
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                return dbContext.Comments.ToList();
            }
        }
        public Comment UpdateComment(Comment comment)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var existingComment = dbContext.Comments.FirstOrDefault(c => c.CommentId == comment.CommentId);
                if (existingComment != null)
                {
                    dbContext.Entry(existingComment).CurrentValues.SetValues(comment);
                    //existingComment.CommentId = comment.CommentId;
                    //existingComment.Content= comment.Content;
                    //existingComment.CommentDate = comment.CommentDate;
                    //existingComment.UserId = comment.UserId;
                    //existingComment.RecipeId = comment.RecipeId;
                    dbContext.SaveChanges();
                }
                return existingComment;
            }
        }
    }
}
