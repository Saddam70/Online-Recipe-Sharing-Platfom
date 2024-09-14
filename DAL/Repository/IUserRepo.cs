using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IUserRepo<T>
    {
        T RegisterUser(T user);
		T DeleteUser(T user);
		T UpdateUserProfile(T user);

        T GetUserProfileById(int id);
        List<T> GetAllUsers();
        T ValidateUser(T user);
        
    }
}
