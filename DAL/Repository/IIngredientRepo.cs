using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IIngredientRepo<T>
    {
        T AddIngredient(T ingredient);

        T UpdateIngredient(T ingredient);

        T DeleteIngredient(int id);

        T GetIngredientById(int id);
        List<T> GelAllIngredient();
    }
}
