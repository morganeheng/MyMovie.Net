using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    interface IMovieDao : IDao<Movie>
    {
        void AddActors(Movie entity, List<Actor> actors);
        void DeleteActors(Movie entity, List<int> Id_actors);
        List<Movie> SelectByTitle(String title);
        List<Movie> SelectByMovieType(String movieType);
    }
}
