using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    public class ImageDao : IImageDao
    {
        public int Create(Image entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Image.Add(entity);
                context.SaveChanges();
                return entity.Id_Image;
            }
        }

        public void Delete(Image entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Image.Attach(entity);
                context.Image.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Modify(Image entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.Image.Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Image SelectOne(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                Image image = context.Image.Where(x => x.Id_Image == id)
                   .Select(x => new {
                       x.Id_Image,
                       x.URL
                   }).ToList()
                   .Select(x => new Image()
                   {
                       Id_Image = x.Id_Image,
                       URL = x.URL
                   }).DefaultIfEmpty().Single();
                return image;
            }
        }

        public List<Image> SelectAll()
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<Image> images = context.Image.Select(x => new {
                    x.Id_Image,
                    x.URL
                }).ToList()
                .Select(x => new Image()
                {
                    Id_Image = x.Id_Image,
                    URL = x.URL
                }).ToList();
                return images;
            }
        }
    }
}
