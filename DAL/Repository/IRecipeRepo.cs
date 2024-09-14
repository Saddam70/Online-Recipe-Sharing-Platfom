using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRecipeRepo<T>
    {
        T AddRecipe(T recipe);

        T UpdateRecipe(T recipe);

        T DeleteRecipe(int id);

        T GetRecipeById(int id);
        List<T> GetAllRecipe();
        int AutoRecipeId();
    }
}