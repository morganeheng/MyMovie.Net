using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MovieNET;

namespace MovieNetWcf
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Facade" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Facade.svc ou Facade.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public sealed class Facade : IFacade
    {
        private static volatile Facade instance;
        private static object syncRoot = new Object();

        ActorDao actorDao = new ActorDao();
        CommentDao commentDao = new CommentDao();
        DirectorDao directorDao = new DirectorDao();
        ImageDao imageDao = new ImageDao();
        MovieDao movieDao = new MovieDao();
        MovieTypeDao movieTypeDao = new MovieTypeDao();
        UserDao userDao = new UserDao();

        private Facade()
        {
        }

        public static Facade Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Facade();
                    }
                }
                return instance;
            }
        }
 
        public void AddMovieActors(int Id_movie, List<int> Id_actors)
        {
            Movie entity = movieDao.SelectOne(Id_movie);
            Actor actor;
            if (entity != null)
            {
                List<Actor> actors = new List<Actor>();

                foreach (int Id_actor in Id_actors)
                {
                    actor = actorDao.SelectOne(Id_actor);
                    if (actor != null)
                        actors.Add(actor);
                }
                movieDao.AddActors(entity, actors);
            }
        }

        public void DeleteMovieActors(int Id_movie, List<int> Id_actors)
        {
            Movie entity = movieDao.SelectOne(Id_movie);
            if (entity != null)
            {
                movieDao.DeleteActors(entity, Id_actors);
            }
        }

        public int CreateActor(string Firstname, string Lastname)
        {
            Actor entity = new Actor();
            entity.Firstname = Firstname;
            entity.Lastname = Lastname;
            return actorDao.Create(entity);
        }

        public int CreateComment(int Id_movie, int Id_user, string Comment1, int Rating)
        {
            Comment entity = new Comment();
            entity.Id_movie = Id_movie;
            entity.Id_user = Id_user;
            entity.Comment1 = Comment1;
            entity.Rating = Rating;
            return commentDao.Create(entity);
        }

        public int CreateDirector(string Firstname, string Lastname)
        {
            Director entity = new Director();
            entity.Firstname = Firstname;
            entity.Lastname = Lastname;
            return directorDao.Create(entity);
        }

        public int CreateImage(string URL)
        {
            Image entity = new Image();
            entity.URL = URL;
            return imageDao.Create(entity);
        }

        public int CreateMovie(string Title, string Synopsis, TimeSpan Duration, int Id_type, int Id_director, int Id_image)
        {
            Movie entity = new Movie();
            entity.Title = Title;
            entity.Synopsis = Synopsis;
            entity.Rating = 0;
            entity.Duration = Duration;
            entity.Id_type = Id_type;
            entity.Id_director = Id_director;
            entity.Id_image = Id_image;
            return movieDao.Create(entity);
        }

        public int CreateMovieType(string Type)
        {
            MovieType entity = new MovieType();
            entity.Type = Type;
            return movieTypeDao.Create(entity);
        }

        public int CreateUser(string Login, string Password)
        {
            User entity = new User();
            entity.Login = Login;
            entity.Password = Password;
            return userDao.Create(entity);
        }

        public void DeleteActor(int Id_actor)
        {
            Actor entity = actorDao.SelectOne(Id_actor);
            if (entity != null)
                actorDao.Delete(entity);
        }

        public void DeleteComment(int Id_comment)
        {
            Comment entity = commentDao.SelectOne(Id_comment);
            if (entity != null)
                commentDao.Delete(entity);
        }

        public void DeleteDirector(int Id_director)
        {
            Director entity = directorDao.SelectOne(Id_director);
            if (entity != null)
                directorDao.Delete(entity);
        }

        public void DeleteImage(int Id_image)
        {
            Image entity = imageDao.SelectOne(Id_image);
            if (entity != null)
                imageDao.Delete(entity);
        }

        public void DeleteMovie(int Id_movie)
        {
            Movie entity = movieDao.SelectOne(Id_movie);
            if (entity != null)
                movieDao.Delete(entity);
        }

        public void DeleteMovieType(int Id_movieType)
        {
            MovieType entity = movieTypeDao.SelectOne(Id_movieType);
            if (entity != null)
                movieTypeDao.Delete(entity);
        }

        public void DeleteUser(int Id_user)
        {
            User entity = userDao.SelectOne(Id_user);
            if (entity != null)
                userDao.Delete(entity);
        }

        public void ModifyActor(int id, string Firstname, string Lastname)
        {
            Actor entity = actorDao.SelectOne(id);
            if (entity != null)
            {
                entity.Firstname = Firstname;
                entity.Lastname = Lastname;
                actorDao.Modify(entity);
            }
        }

        public void ModifyComment(int id, int Id_movie, int Id_user, string Comment1, int Rating)
        {
            Comment entity = commentDao.SelectOne(id);
            if (entity != null)
            {
                entity.Id_movie = Id_movie;
                entity.Id_user = Id_user;
                entity.Comment1 = Comment1;
                entity.Rating = Rating;
                commentDao.Modify(entity);
            }
        }

        public void ModifyDirector(int id, string Firstname, string Lastname)
        {
            Director entity = directorDao.SelectOne(id);
            if (entity != null)
            {
                entity.Firstname = Firstname;
                entity.Lastname = Lastname;
                directorDao.Modify(entity);
            }
        }

        public void ModifyImage(int id, string URL)
        {
            Image entity = imageDao.SelectOne(id);
            if (entity != null)
            {
                entity.URL = URL;
                imageDao.Modify(entity);
            }
        }

        public void ModifyMovie(int id, string Title, string Synopsis, TimeSpan Duration, int Id_type, int Id_director, int Id_image)
        {
            Movie entity = movieDao.SelectOne(id);
            if (entity != null)
            {
                entity.Title = Title;
                entity.Synopsis = Synopsis;
                entity.Duration = Duration;
                entity.Id_type = Id_type;
                entity.Id_director = Id_director;
                entity.Id_image = Id_image;
                movieDao.Modify(entity);
            }
        }

        public void ModifyMovieType(int id, string Type)
        {
            MovieType entity = movieTypeDao.SelectOne(id);
            if (entity != null)
            {
                entity.Type = Type;
                movieTypeDao.Modify(entity);
            }
        }

        public void ModifyUser(int id, string Login, string Password)
        {
            User entity = userDao.SelectOne(id);
            if (entity != null)
            {
                entity.Login = Login;
                entity.Password = Password;
                userDao.Modify(entity);
            }
        }

        public List<Actor> SelectActorByMovie(int id)
        {
            return actorDao.SelectByMovie(id);
        }

        public List<Actor> SelectAllActor()
        {
            return actorDao.SelectAll();
        }

        public List<Comment> SelectAllComment()
        {
            return commentDao.SelectAll();
        }

        public List<Director> SelectAllDirector()
        {
            return directorDao.SelectAll();
        }

        public List<Image> SelectAllImage()
        {
            return imageDao.SelectAll();
        }

        public List<Movie> SelectAllMovie()
        {
            return movieDao.SelectAll();
        }

        public List<MovieType> SelectAllMovieType()
        {
            return movieTypeDao.SelectAll();
        }

        public List<User> SelectAllUser()
        {
            return userDao.SelectAll();
        }

        public List<Comment> SelectCommentByMovie(int id)
        {
            return commentDao.SelectByMovie(id);
        }

        public List<Movie> SelectMovieByMovieType(string movieType)
        {
            return movieDao.SelectByMovieType(movieType);
        }

        public List<Movie> SelectMovieByTitle(string title)
        {
            return movieDao.SelectByTitle(title);
        }

        public Actor SelectOneActor(int id)
        {
            return actorDao.SelectOne(id);
        }

        public Comment SelectOneComment(int id)
        {
            return commentDao.SelectOne(id);
        }

        public Director SelectOneDirector(int id)
        {
            return directorDao.SelectOne(id);
        }

        public Image SelectOneImage(int id)
        {
            return imageDao.SelectOne(id);
        }

        public Movie SelectOneMovie(int id)
        {
            return movieDao.SelectOne(id);
        }

        public MovieType SelectOneMovieType(int id)
        {
            return movieTypeDao.SelectOne(id);
        }

        public User SelectOneUser(int id)
        {
            return userDao.SelectOne(id);
        }

        public bool CheckUser(string Login, string Password)
        {
            return userDao.CheckUser(Login, Password);
        }

        public bool CheckUserExist(string Login)
        {
            return userDao.CheckUserExist(Login);
        }

        public double SelectRating(int id)
        {
            return commentDao.SelectRating(id);
        }

        public int SelectIdByLogin(string Login)
        {
            return userDao.SelectIdByLogin(Login);
        }
    }
}
