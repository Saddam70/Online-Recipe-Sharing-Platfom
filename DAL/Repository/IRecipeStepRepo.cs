using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRecipeStepRepo<T>
    {
        T AddRecipeStep(T recipestep);

        T UpdateRecipeStep(T recipestep);

        T DeleteRecipeStep(int id);

        T GetRecipeStepById(int id);
		List<T> GetAllRecipeStep();
		
	}
}
