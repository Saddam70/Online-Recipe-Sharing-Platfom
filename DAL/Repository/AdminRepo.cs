using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class AdminRepo : IAdminRepo<Admin>
    {
        public Admin ValidateAdmin(Admin admin)
        {
            using (RecipeDbContext dbContext = new RecipeDbContext())
            {
                var ads = dbContext.Admins.Where(ads => ads.EmailId.Equals(admin.EmailId) && ads.Password.Equals(admin.Password) && ads.Role.Equals(admin.Role)).FirstOrDefault();
                return ads;
            }
        }
    }
}
