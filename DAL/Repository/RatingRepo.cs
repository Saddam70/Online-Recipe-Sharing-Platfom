using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repository;

namespace DAL.Repository
{
    public class RatingRepo : IRatingRepo<Rating>
    {
		//public Rating AddRating(Rating rating);
		//public Rating DeleteRating(int id);
		//public Rating GetRatingById(int id);
		//public IEnumerable<Rating> GetAllRating();
		//public Rating UpdateRating(Rating rating);
		public Rating AddRating(Rating rating)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                dbContext.Ratings.Add(rating);
                dbContext.SaveChanges();
                return rating;
            }
        }

        public Rating DeleteRating(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var rating = dbContext.Ratings.Find(id);
                if (rating != null)
                {
                    dbContext.Ratings.Remove(rating);
                    dbContext.SaveChanges();
                }
                return rating;
            }
        }

        public Rating GetRatingById(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                return dbContext.Ratings.Find(id);
            }
        }
        public List<Rating> GetAllRating()
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                return dbContext.Ratings.ToList();
            }
        }
        public Rating UpdateRating(Rating rating)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                dbContext.Ratings.Update(rating);

                dbContext.SaveChanges();
                return rating;
            }
            //using (RecipeDbContext dbContext = new RecipeDbContext())
            //{
            //    var existingRating = dbContext.Ratings.Find(rating.Id);
            //    if (existingRating != null)
            //    {
            //        // Manually update properties
            //        existingRating.Score = rating.Score;
            //        existingRating.Comment = rating.Comment;
            //        existingRating.RecipeId = rating.RecipeId;
            //        existingRating.UserId = rating.UserId;

            //        dbContext.SaveChanges();
            //    }
            //    return existingRating;
            //}
        }
    }
}
