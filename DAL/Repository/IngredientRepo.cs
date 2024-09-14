using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class IngredientRepo : IIngredientRepo<Ingredient>
    {
        public Ingredient AddIngredient(Ingredient ingredient)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                dbContext.Ingredients.Add(ingredient);
                dbContext.SaveChanges();
                return ingredient;
            }
        }

        public Ingredient DeleteIngredient(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var ingredient = dbContext.Ingredients.FirstOrDefault(i => i.IngredientId == id);
                if (ingredient != null)
                {
                    dbContext.Ingredients.Remove(ingredient);
                    dbContext.SaveChanges();
                }
                return ingredient;
            }
        }

        public Ingredient GetIngredientById(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                return dbContext.Ingredients.FirstOrDefault(i => i.IngredientId == id);
            }
        }
		public List<Ingredient> GelAllIngredient()
		{
			using (RecipeDbContext dbContext = new RecipeDbContext())
			{
				var ingredientlist = dbContext.Ingredients.ToList();
				return ingredientlist;
			}
		}
		public Ingredient UpdateIngredient(Ingredient ingredient)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var existingIngredient = dbContext.Ingredients.FirstOrDefault(i => i.IngredientId == ingredient.IngredientId);
                if (existingIngredient != null)
                {
                    dbContext.Entry(existingIngredient).CurrentValues.SetValues(ingredient);
                    //existingIngredient.IngredientId = ingredient.IngredientId;
                    //existingIngredient.Name = ingredient.Name;
                    //existingIngredient.Quantity = ingredient.Quantity;
                    //existingIngredient.RecipeId = ingredient.RecipeId;
                    dbContext.SaveChanges();
                }
                return existingIngredient;
            }
        }

		
	}
}
