using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    interface IActorDao : IDao<Actor>
    {
        List<Actor> SelectByMovie(int id);
    }
}
