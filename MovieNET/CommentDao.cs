using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    public class CommentDao : ICommentDao
    {
        public int Create(Comment entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Comment.Add(entity);
                context.SaveChanges();
                return entity.Id_comment;
            }
        }

        public void Delete(Comment entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Comment.Attach(entity);
                context.Comment.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Modify(Comment entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Comment.Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Comment SelectOne(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                Comment comment = context.Comment.Where(x => x.Id_comment == id)
                   .Select(x => new {
                       x.Id_comment,
                       x.Id_movie,
                       x.Id_user,
                       x.Comment1,
                       x.Rating
                   }).ToList()
                   .Select(x => new Comment()
                   {
                       Id_comment = x.Id_comment,
                       Id_movie = x.Id_movie,
                       Id_user = x.Id_user,
                       Comment1 = x.Comment1,
                       Rating = x.Rating
                   }).DefaultIfEmpty().Single();
                return comment;
            }
        }

        public List<Comment> SelectAll()
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Comment> comments = context.Comment.Select(x => new {
                    x.Id_comment,
                    x.Id_movie,
                    x.Id_user,
                    x.Comment1,
                    x.Rating
                }).ToList()
                .Select(x => new Comment()
                {
                    Id_comment = x.Id_comment,
                    Id_movie = x.Id_movie,
                    Id_user = x.Id_user,
                    Comment1 = x.Comment1,
                    Rating = x.Rating
                }).ToList();
                return comments;
            }
        }

        public List<Comment> SelectByMovie(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Comment> comments = context.Comment
                .Where(c => c.Id_movie == id)
                .Select(x => new {
                    x.Id_comment,
                    x.Id_movie,
                    x.Id_user,
                    x.Comment1,
                    x.Rating
                }).ToList()
                .Select(x => new Comment()
                {
                    Id_comment = x.Id_comment,
                    Id_movie = x.Id_movie,
                    Id_user = x.Id_user,
                    Comment1 = x.Comment1,
                    Rating = x.Rating
                }).ToList();
                return comments;
            }
        }

        public double SelectRating(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                double rating = context.Comment
                .Where(c => c.Id_movie == id)
                .Select(x => x.Rating)
                .DefaultIfEmpty()
                .Average();
                return rating;
            }
        }
    }
}
