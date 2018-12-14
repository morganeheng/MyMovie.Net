using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    public class MovieDao : IMovieDao
    {
        public int Create(Movie entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Movie.Add(entity);
                context.SaveChanges();
                return entity.Id_movie;
            }
        }

        public void Delete(Movie entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Movie.Attach(entity);
                context.Entry(entity).Collection("Comment").Load();
                context.Entry(entity).Collection("Actor").Load();
                List<Comment> comments = entity.Comment.ToList();
                List<Actor> actors = entity.Actor.ToList();
                foreach (Comment comment in comments)
                {
                    context.Comment.Remove(comment);
                }

                foreach (Actor actor in actors)
                {
                    entity.Actor.Remove(actor);
                }
                context.Movie.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Modify(Movie entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Movie.Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Movie SelectOne(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                Movie movie = context.Movie.Where(x => x.Id_movie == id)
                   .Select(x => new {
                       x.Id_movie,
                       x.Title,
                       x.Synopsis,
                       x.Rating,
                       x.Duration,
                       x.Id_type,
                       x.Id_director,
                       x.Id_image
                   }).ToList()
                   .Select(x => new Movie()
                   {
                       Id_movie = x.Id_movie,
                       Title = x.Title,
                       Synopsis = x.Synopsis,
                       Rating = x.Rating,
                       Duration = x.Duration,
                       Id_type = x.Id_type,
                       Id_director = x.Id_director,
                       Id_image = x.Id_image
                   }).DefaultIfEmpty().Single();
                return movie;
            }
        }

        public List<Movie> SelectAll()
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Movie> movies = context.Movie.Select(x => new {
                    x.Id_movie,
                    x.Title,
                    x.Synopsis,
                    x.Rating,
                    x.Duration,
                    x.Id_type,
                    x.Id_director,
                    x.Id_image
                }).ToList()
                .Select(x => new Movie()
                {
                    Id_movie = x.Id_movie,
                    Title = x.Title,
                    Synopsis = x.Synopsis,
                    Rating = x.Rating,
                    Duration = x.Duration,
                    Id_type = x.Id_type,
                    Id_director = x.Id_director,
                    Id_image = x.Id_image
                }).ToList();
                return movies;
            }
        }

        public void AddActors(Movie entity, List<Actor> actors)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Movie.Attach(entity);
                foreach (Actor actor in actors)
                {
                    context.Actor.Attach(actor);
                    context.Entry(actor).State = System.Data.Entity.EntityState.Unchanged;
                    entity.Actor.Add(actor);
                }
                context.Entry(entity).State = System.Data.Entity.EntityState.Unchanged;
                context.SaveChanges();
            }
        }

        public void DeleteActors(Movie entity, List<int> Id_actors)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Movie.Attach(entity);
                context.Entry(entity).Collection("Actor").Load();
                foreach (int Id_actor in Id_actors)
                {
                    entity.Actor.Remove(entity.Actor.FirstOrDefault(s => s.Id_actor == Id_actor));
                }
                context.SaveChanges();
            }
        }

        public List<Movie> SelectByTitle(String title)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Movie> movies = context.Movie.Where(m => m.Title.Contains(title))
                .Select(x => new {
                    x.Id_movie,
                    x.Title,
                    x.Synopsis,
                    x.Rating,
                    x.Duration,
                    x.Id_type,
                    x.Id_director,
                    x.Id_image
                }).ToList()
                .Select(x => new Movie()
                {
                    Id_movie = x.Id_movie,
                    Title = x.Title,
                    Synopsis = x.Synopsis,
                    Rating = x.Rating,
                    Duration = x.Duration,
                    Id_type = x.Id_type,
                    Id_director = x.Id_director,
                    Id_image = x.Id_image
                }).ToList();
                return movies;
            }
        }

        public List<Movie> SelectByMovieType(String movieType)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Movie> movies = context.Movie
                .Join(context.MovieType, 
                      m => m.Id_type, 
                      mt => mt.Id_type, 
                      (m, mt) => new {m, mt})
                .Where(movietype => movietype.mt.Type.Contains(movieType))
                .Select(movie => movie.m)
                .ToList()
                .Select(x => new {
                    x.Id_movie,
                    x.Title,
                    x.Synopsis,
                    x.Rating,
                    x.Duration,
                    x.Id_type,
                    x.Id_director,
                    x.Id_image
                }).ToList()
                .Select(x => new Movie()
                {
                    Id_movie = x.Id_movie,
                    Title = x.Title,
                    Synopsis = x.Synopsis,
                    Rating = x.Rating,
                    Duration = x.Duration,
                    Id_type = x.Id_type,
                    Id_director = x.Id_director,
                    Id_image = x.Id_image
                }).ToList();
                return movies;
            }
        }
    }
}
