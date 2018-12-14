using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MovieNetWpf.ServiceFacade;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MovieNetWpf.ViewModel
{
    class FilmViewModel : ViewModelBase
    {
        private int id_movie;
        private FacadeClient serviceClient = new FacadeClient();
        private List<MovieNET.Actor> listActor;
        private List<MovieNET.Comment> listComment;
        private string title;
        private string synopsis;
        private string image;
        private string director;
        private string ldirector;
        private string type;
        private string comment;
        private string usercomment;
        private int movierating;
        private MovieNET.Movie MovieSelected;
        private MovieNET.MovieType MovieType;
        private MovieNET.Director MovieDirector;
        private MovieNET.Image MovieImage;
        private String MovieTitle;
        private String MovieSynopsis;
        private int MovieDurationHours;
        private int MovieDurationMin;
        private int MovieDurationSec;
        private double note;
        private double rating;
        private MovieNET.User User_co;
        private string movieComment;

        public FilmViewModel()
        {
            CommandAddComment = new RelayCommand(CommandAddCommentExecute);
            CommandReturn = new RelayCommand<Page>(CommandReturnExecute);
            Messenger.Default.Register<MovieNET.User>(this, (user_co) =>
            {
                User_co = user_co;
            });

            Messenger.Default.Register<int>(this, (ID_movie) =>
            {
                id_movie = ID_movie;
                MovieSelected = serviceClient.SelectOneMovie(id_movie);
                if (MovieSelected != null)
                {
                    MovieType = serviceClient.SelectOneMovieType(MovieSelected.Id_type);
                    MovieDirector = serviceClient.SelectOneDirector(MovieSelected.Id_director);
                    MovieImage = serviceClient.SelectOneImage(MovieSelected.Id_image);
                    MovieTitle = MovieSelected.Title;
                    MovieSynopsis = MovieSelected.Synopsis;
                    MovieDurationHours = MovieSelected.Duration.Hours;
                    MovieDurationMin = MovieSelected.Duration.Minutes;
                    MovieDurationSec = MovieSelected.Duration.Seconds;
                    ListActor = serviceClient.SelectActorByMovie(id_movie);
                    ListComment = serviceClient.SelectCommentByMovie(id_movie);
                    rating = serviceClient.SelectRating(id_movie);

                    Title = MovieTitle;
                    Synopsis = MovieSynopsis;
                    Image = MovieImage.URL;
                    Director = MovieDirector.Firstname;
                    LDirector = MovieDirector.Lastname;
                    Type = MovieType.Type;
                    Note = rating;

                }
            });
        }

        public RelayCommand CommandAddComment { get; }

        void CommandAddCommentExecute()
        {
            if (MovieComment != null)
            {
                if (MovieRating >= 0 && MovieRating <= 5)
                {
                    serviceClient.CreateComment(id_movie, User_co.Id_user, MovieComment, MovieRating);
                    MessageBox.Show("Commentaire ajouté !");
                    clearPage();
                    ListComment = serviceClient.SelectCommentByMovie(id_movie);
                    rating = serviceClient.SelectRating(id_movie);
                    Note = rating;
                }
                else
                {
                    MessageBox.Show("La note doit être comprise entre 0 et 5");
                }
            }
            else
            {
                MessageBox.Show("Vous n'avez pas ajouter de commentaire");
            }
            
        }

        public RelayCommand<Page> CommandReturn { get; }

        void CommandReturnExecute(Page page)
        {
            Messenger.Default.Send(User_co);
            var PageW = Window.GetWindow(page);
            PageW.Content = new View.SearchPage();
        }

        void clearPage()
        {
            MovieComment = "";
            MovieRating = 0;
        }

        public double Note
        {
            get { return note; }
            set
            {
                if (value != note)
                {
                    note = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string MovieUserComment
        {
            get { return usercomment; }
            set
            {
                if (value != usercomment)
                {
                    usercomment = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string MovieComment
        {
            get { return movieComment; }
            set
            {
                if (value != movieComment)
                {
                    movieComment = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int MovieRating
        {
            get { return movierating; }
            set
            {
                if (value != movierating)
                {
                    movierating = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string TextComment
        {
            get { return comment; }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    title = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Synopsis
        {
            get { return synopsis; }
            set
            {
                if (value != synopsis)
                {
                    synopsis = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String Image
        {
            get { return image; }
            set
            {
                if (value != image)
                {
                    image = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String Type
        {
            get { return type; }
            set
            {
                if (value != type)
                {
                    type = value;
                    RaisePropertyChanged();
                }
            }
        }

        public List<MovieNET.Actor> ListActor
        {
            get { return listActor; }
            set
            {
                if (value != listActor)
                {
                    listActor = value;
                    RaisePropertyChanged();
                }
            }
        }

        public List<MovieNET.Comment> ListComment
        {
            get { return listComment; }
            set
            {
                if (value != listComment)
                {
                    listComment = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String Director
        {
            get { return director; }
            set
            {
                if (value != director)
                {
                    director = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String LDirector
        {
            get { return ldirector; }
            set
            {
                if (value != ldirector)
                {
                    ldirector = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}