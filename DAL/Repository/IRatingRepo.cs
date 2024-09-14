using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRatingRepo<T>
    {

        T AddRating(T rating);

        T UpdateRating(T rating);

        T DeleteRating(int id);

        T GetRatingById(int id);
       List<T> GetAllRating();
    }
}
