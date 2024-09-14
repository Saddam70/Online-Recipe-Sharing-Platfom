using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RecipeStepRepo : IRecipeStepRepo<RecipeStep>
    {
		//public RecipeStep AddRecipeStep(RecipeStep recipestep);
		//public RecipeStep DeleteRecipeStep(int id);
		//public RecipeStep GetRecipeStepById(int id);
		//public IEnumerable<RecipeStep> GetAllRecipeSteps();
		//public RecipeStep UpdateRecipeStep(RecipeStep recipestep);
		public RecipeStep AddRecipeStep(RecipeStep recipestep)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                dbContext.RecipeSteps.Add(recipestep);
                dbContext.SaveChanges();
                return recipestep;
            }
        }

        public RecipeStep DeleteRecipeStep(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var recipestep = dbContext.RecipeSteps.FirstOrDefault(r => r.RecipeStepId == id);

                if (recipestep != null)
                {
                    dbContext.RecipeSteps.Remove(recipestep);
                    dbContext.SaveChanges();
                    return recipestep;
                }

                return null;
            }
        }

        public RecipeStep GetRecipeStepById(int id)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                return dbContext.RecipeSteps.FirstOrDefault(rs => rs.RecipeStepId == id);
            }
        }
		public List<RecipeStep> GetAllRecipeStep()
		{
			using (RecipeDbContext dbContext = new RecipeDbContext())
			{
				var recipesteplist = dbContext.RecipeSteps.ToList();
				return recipesteplist;
			}
		}
		public RecipeStep UpdateRecipeStep(RecipeStep recipestep)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var existingRecipeStep = dbContext.RecipeSteps.FirstOrDefault(rs => rs.RecipeStepId == recipestep.RecipeStepId);
                if (existingRecipeStep != null)
                {
                    dbContext.Entry(existingRecipeStep).CurrentValues.SetValues(recipestep);
                    //existingRecipeStep.RecipeStepId = recipestep.RecipeStepId;
                    //existingRecipeStep.StepNumber = recipestep.StepNumber;
                    //existingRecipeStep.Instruction = recipestep.Instruction;
                    //existingRecipeStep.ImageUrl = recipestep.ImageUrl;
                    //existingRecipeStep.RecipeId = recipestep.RecipeId;
                    dbContext.SaveChanges();
                }
                return existingRecipeStep;
            }
        }

		
	}
}
