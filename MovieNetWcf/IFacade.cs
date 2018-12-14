using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MovieNET;

namespace MovieNetWcf
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IFacade" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IFacade
    {
        [OperationContract]
        int CreateActor(string Firstname, string Lastname);
        [OperationContract]
        void DeleteActor(int Id_actor);
        [OperationContract]
        void ModifyActor(int id, string Firstname, string Lastname);
        [OperationContract]
        Actor SelectOneActor(int id);
        [OperationContract]
        List<Actor> SelectAllActor();
        [OperationContract]
        List<Actor> SelectActorByMovie(int id);

        [OperationContract]
        int CreateComment(int Id_movie, int Id_user, string Comment1, int Rating);
        [OperationContract]
        void DeleteComment(int Id_comment);
        [OperationContract]
        void ModifyComment(int id, int Id_movie, int Id_user, string Comment1, int Rating);
        [OperationContract]
        Comment SelectOneComment(int id);
        [OperationContract]
        List<Comment> SelectAllComment();
        [OperationContract]
        List<Comment> SelectCommentByMovie(int id);
        [OperationContract]
        double SelectRating(int id);

        [OperationContract]
        int CreateDirector(string Firstname, string Lastname);
        [OperationContract]
        void DeleteDirector(int Id_director);
        [OperationContract]
        void ModifyDirector(int id, string Firstname, string Lastname);
        [OperationContract]
        Director SelectOneDirector(int id);
        [OperationContract]
        List<Director> SelectAllDirector();

        [OperationContract]
        int CreateImage(string URL);
        [OperationContract]
        void DeleteImage(int Id_image);
        [OperationContract]
        void ModifyImage(int id, string URL);
        [OperationContract]
        Image SelectOneImage(int id);
        [OperationContract]
        List<Image> SelectAllImage();

        [OperationContract]
        int CreateMovie(string Title, string Synopsis, TimeSpan Duration, int Id_type, int Id_director, int Id_image);
        [OperationContract]
        void DeleteMovie(int Id_movie);
        [OperationContract]
        void ModifyMovie(int id, string Title, string Synopsis, TimeSpan Duration, int Id_type, int Id_director, int Id_image);
        [OperationContract]
        Movie SelectOneMovie(int id);
        [OperationContract]
        List<Movie> SelectAllMovie();
        [OperationContract]
        void AddMovieActors(int Id_movie, List<int> Id_actors);
        [OperationContract]
        void DeleteMovieActors(int Id_movie, List<int> Id_actors);
        [OperationContract]
        List<Movie> SelectMovieByTitle(String title);
        [OperationContract]
        List<Movie> SelectMovieByMovieType(String movieType);

        [OperationContract]
        int CreateMovieType(string Type);
        [OperationContract]
        void DeleteMovieType(int Id_movieType);
        [OperationContract]
        void ModifyMovieType(int id, string Type);
        [OperationContract]
        MovieType SelectOneMovieType(int id);
        [OperationContract]
        List<MovieType> SelectAllMovieType();

        [OperationContract]
        int CreateUser(string Login, string Password);
        [OperationContract]
        void DeleteUser(int Id_user);
        [OperationContract]
        void ModifyUser(int id, string Login, string Password);
        [OperationContract]
        User SelectOneUser(int id);
        [OperationContract]
        List<User> SelectAllUser();
        [OperationContract]
        bool CheckUser(string Login, string Password);
        [OperationContract]
        bool CheckUserExist(string Login);
        [OperationContract]
        int SelectIdByLogin(string Login);
    }
}
