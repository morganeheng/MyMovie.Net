using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    public  class DirectorDao : IDirectorDao
    {
        public int Create(Director entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Director.Add(entity);
                context.SaveChanges();
                return entity.Id_director;
            }
        }

        public void Delete(Director entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Director.Attach(entity);
                context.Director.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Modify(Director entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Director.Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Director SelectOne(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                Director director = context.Director.Where(x => x.Id_director == id)
                   .Select(x => new {
                       x.Id_director,
                       x.Firstname,
                       x.Lastname
                   }).ToList()
                   .Select(x => new Director()
                   {
                       Id_director = x.Id_director,
                       Firstname = x.Firstname,
                       Lastname = x.Lastname
                   }).DefaultIfEmpty().Single();
                return director;
            }
        }

        public List<Director> SelectAll()
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Director> directors = context.Director.Select(x => new {
                    x.Id_director,
                    x.Firstname,
                    x.Lastname
                }).ToList()
                .Select(x => new Director()
                {
                    Id_director = x.Id_director,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname
                }).ToList();
                return directors;
            }
        }
    }
}
