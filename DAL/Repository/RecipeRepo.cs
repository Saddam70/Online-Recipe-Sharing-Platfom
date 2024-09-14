using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class RecipeRepo : IRecipeRepo<Recipe>
    {

		public Recipe AddRecipe(Recipe recipe)
		{
			using (RecipeDbContext dbContext = new RecipeDbContext())
			{
				//recipe.RecipeId = AutoRecipeId();
				dbContext.Recipes.Add(recipe);
				dbContext.SaveChanges();
				return recipe;
			}
		}

		public Recipe DeleteRecipe(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var recipe = dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);

                if (recipe != null)
                {
                    dbContext.Recipes.Remove(recipe);
                    dbContext.SaveChanges();
                    return recipe;
                }

                return null;
            }
        }

        public List<Recipe> GetAllRecipe()
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var recipelist = dbContext.Recipes.ToList();
                return recipelist;
            }
        }

public Recipe GetRecipeById(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                return dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);
                //var existingPatient = dbContext.Patients.Where(e => e.PatientId.Equals(id)).FirstOrDefault();
                //return existingPatient;
            }
        }

        public Recipe UpdateRecipe(Recipe recipe)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var existingRecipe = dbContext.Recipes.FirstOrDefault(r => r.RecipeId == recipe.RecipeId);

              
					//dbContext.Entry(existingRecipe).CurrentValues.SetValues(recipe);
					existingRecipe.Title = recipe.Title;
					existingRecipe.Description = recipe.Description;
					existingRecipe.Cuisine = recipe.Cuisine;
					
					existingRecipe.Difficulty = recipe.Difficulty;
					existingRecipe.CookingTime = recipe.CookingTime;
					existingRecipe.UserId = recipe.UserId;
					dbContext.SaveChanges();
                    return existingRecipe;
               

                //return existingRecipe;
            }
        }
		//public int AutoRecipeId()
		//{
		//    using (RecipeDbContext dbContext = new RecipeDbContext())
		//    {
		//        // Get the max product_id from the Products table using LINQ
		//        int? maxRecipeId = dbContext.Recipes.Max(p => (int?)p.RecipeId);

		//        // Increment the max product ID by 1 to generate the next ID
		//        int nextProductId = (maxRecipeId ?? 0) + 1;

		//        return nextProductId;
		//    }
		//}
		//public int AutoRecipeId()
		//{
		//    using (RecipeDbContext dbContext = new RecipeDbContext())
		//    {
		//        int nextRecipeId = 0;
		//        try
		//        {
		//            // Query the database to get the maximum ProductId
		//            nextRecipeId = dbContext.Recipes.Max(p => (int?)p.RecipeId) ?? 0;
		//            nextRecipeId++; // Increment to get the next ProductId
		//        }
		//        catch (Exception ex)
		//        {
		//            // Handle the exception (e.g., logging)
		//            throw new Exception("Error fetching next Recipe ID", ex);
		//        }

		//        return nextRecipeId;
		//    }
		//}
		public int AutoRecipeId()
		{
			using (RecipeDbContext dbContext = new RecipeDbContext())
			{
				int nextRecipeId = 0;
				try
				{
					// Query the database to get the maximum RecipeId
					nextRecipeId = dbContext.Recipes.Max(p => (int?)p.RecipeId) ?? 0;
					nextRecipeId++; // Increment to get the next RecipeId
				}
				catch (Exception ex)
				{
					// Handle the exception (e.g., logging)
					throw new Exception("Error fetching next Recipe ID", ex);
				}

				return nextRecipeId;
			}
		}
	}
}
