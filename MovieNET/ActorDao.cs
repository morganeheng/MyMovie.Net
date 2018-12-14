using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    public class ActorDao : IActorDao
    {
        public int Create(Actor entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Actor.Add(entity);
                context.SaveChanges();
                return entity.Id_actor;
            }
        }

        public void Delete(Actor entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Actor.Attach(entity);
                context.Entry(entity).Collection("Movie").Load();
                List<Movie> movies = entity.Movie.ToList();
                foreach (Movie movie in movies)
                {
                    entity.Movie.Remove(movie);
                }
                context.Actor.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Modify(Actor entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Actor.Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Actor SelectOne(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                Actor actor = context.Actor.Where(x => x.Id_actor == id)
                   .Select(x => new {
                       x.Id_actor,
                       x.Firstname,
                       x.Lastname
                   }).ToList()
                   .Select(x => new Actor()
                   {
                       Id_actor = x.Id_actor,
                       Firstname = x.Firstname,
                       Lastname = x.Lastname
                   }).DefaultIfEmpty().Single();
                return actor;
            }
        }

        public List<Actor> SelectAll()
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Actor> actors = context.Actor.Select(x => new {
                    x.Id_actor,
                    x.Firstname,
                    x.Lastname
                }).ToList()
                .Select(x => new Actor()
                {
                    Id_actor = x.Id_actor,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname
                }).ToList();
                return actors;
            }
        }

        public List<Actor> SelectByMovie(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Actor> actors = context.Actor
                .Where(a => a.Movie.Any(m => m.Id_movie == id))
                .Select(x => new {
                    x.Id_actor,
                    x.Firstname,
                    x.Lastname
                }).ToList()
                .Select(x => new Actor()
                {
                    Id_actor = x.Id_actor,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname
                }).ToList();
                return actors;
            }
        }
    }
}
