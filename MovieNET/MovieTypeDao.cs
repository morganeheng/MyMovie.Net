using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    public class MovieTypeDao : IMovieTypeDao
    {
        public int Create(MovieType entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.MovieType.Add(entity);
                context.SaveChanges();
                return entity.Id_type;
            }
        }

        public void Delete(MovieType entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.MovieType.Attach(entity);
                context.MovieType.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Modify(MovieType entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.MovieType.Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public MovieType SelectOne(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                MovieType movieType = context.MovieType.Where(x => x.Id_type == id)
                   .Select(x => new {
                       x.Id_type,
                       x.Type
                   }).ToList()
                   .Select(x => new MovieType()
                   {
                       Id_type = x.Id_type,
                       Type = x.Type
                   }).DefaultIfEmpty().Single();
                return movieType;
            }
        }

        public List<MovieType> SelectAll()
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<MovieType> movieTypes = context.MovieType.Select(x => new {
                    x.Id_type,
                    x.Type
                }).ToList()
                .Select(x => new MovieType()
                {
                    Id_type = x.Id_type,
                    Type = x.Type
                }).ToList();
                return movieTypes;
            }
        }
    }
}
