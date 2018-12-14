using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    public class UserDao : IUserDao
    {
        public int Create(User entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.User.Add(entity);
                context.SaveChanges();
                return entity.Id_user;
            }
        }

        public void Delete(User entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.User.Attach(entity);
                context.User.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Modify(User entity)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                context.User.Attach(entity);
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public User SelectOne(int id)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                User user = context.User.Where(x => x.Id_user == id)
                   .Select(x => new {
                       x.Id_user,
                       x.Login,
                       x.Password
                   }).ToList()
                   .Select(x => new User()
                   {
                       Id_user = x.Id_user,
                       Login = x.Login,
                       Password = x.Password
                   }).DefaultIfEmpty().Single();
                return user;
            }
        }

        public List<User> SelectAll()
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                List<User> users = context.User.Select(x => new {
                    x.Id_user,
                    x.Login,
                    x.Password
                }).ToList()
                .Select(x => new User()
                {
                    Id_user = x.Id_user,
                    Login = x.Login,
                    Password = x.Password
                }).ToList();
                return users;
            }
        }

        public bool CheckUser(string Login, string Password)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                int nbUser = context.User.Where(u => u.Login == Login && u.Password == Password).Count();
                if (nbUser != 0)
                    return true;
                else
                    return false;
            }
        }

        public bool CheckUserExist(string Login)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                int nbUser = context.User.Where(u => u.Login == Login).Count();
                if (nbUser != 0)
                    return true;
                else
                    return false;
            }
        }

        public int SelectIdByLogin(string Login)
        {
            using (MovieLibraryEntities context = new MovieLibraryEntities())
            {
                int id_user = context.User.Where(u => u.Login == Login)
                   .Select(x => x.Id_user)
                   .First();
                return id_user;
            }
        }
    }
}
