using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ICommentRepo<T>
    {
        T AddComment(T comment);

        T UpdateComment(T comment);

        T DeleteComment(int id);

        T GetCommentById(int id);
        List<T> GetAllComment();
    }
}
