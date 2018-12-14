using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    interface ICommentDao : IDao<Comment>
    {
        List<Comment> SelectByMovie(int id);
        double SelectRating(int id);
    }
}
