using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UserRepo : IUserRepo<User>
    {
        public User DeleteUser(User user)
        {
            try
            {
                using (RecipeDbContext dbContext = new RecipeDbContext())
                {

                    //var existingUser = dbContext.Users.FirstOrDefault(cust => cust.UserId.Equals(user.UserId));

                    //if (existingUser != null)
                    //{
                        dbContext.Users.Remove(user);
                        dbContext.SaveChanges();
                        return user;
                    //}
                    //else
                    //{
                    //    return null;
                    //}

                }
            }
            
              catch (Exception ex)
               {
                // Log the exception (you can log the exception message if you have a logger)
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }

        public User RegisterUser(User user)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return user;
            }
        }

        public User UpdateUserProfile(User user)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                //var existingUser = dbContext.Users.Where(cust => cust.EmailId.Equals(cust.EmailId)).FirstOrDefault();

                //existingUser.Password = user.Password;
                //dbContext.SaveChanges();
                //return existingUser;
                var updateuser=dbContext.Users.Where(u=>u.UserId.Equals(user.UserId)).FirstOrDefault();
                
                updateuser.Username= user.Username;
                updateuser.EmailId=user.EmailId;
                updateuser.Password = user.Password;
                updateuser.Role = user.Role;
                updateuser.Date=user.Date;
                updateuser.Gender = user.Gender;
                updateuser.FavoriteCuisines = user.FavoriteCuisines;
                updateuser.DietaryPreferences = user.DietaryPreferences;
                dbContext.SaveChanges();
                return updateuser;
            }
        }

        public User ValidateUser(User user)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                //var userInfo = dbContext.Users.Where(us => us.EmailId.Equals(user.EmailId) && us.Password.Equals(user.Password) && us.Role.Equals(user.Role)).FirstOrDefault();
                var userInfo = dbContext.Users.FirstOrDefault(u => u.EmailId == user.EmailId && u.Password == user.Password && u.Role == user.Role);
                return userInfo;
            }
        }

		public List<User> GetAllUsers()
		{
			using(RecipeDbContext dbContext = new RecipeDbContext()) 
            {
                var users= dbContext.Users.ToList();
                return users;
            }
		}

		public User GetUserProfileById(int id)
		{
			using(RecipeDbContext context = new RecipeDbContext())
            {
                var exituser=context.Users.Where(u=>u.UserId.Equals(id)).FirstOrDefault();
                return exituser;
            }
		}

   

    }
}
