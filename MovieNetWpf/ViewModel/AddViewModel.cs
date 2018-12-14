using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MovieNetWpf.ServiceFacade;
using System.Windows.Controls;
using System.Windows;
using Xceed.Wpf.Toolkit;
using GalaSoft.MvvmLight.Messaging;

namespace MovieNetWpf.ViewModel
{
    class AddViewModel : ViewModelBase
    {
        private FacadeClient serviceClient = new FacadeClient();
        private List<MovieNET.MovieType> listMovieType;
        private List<MovieNET.Actor> listActor;
        private List<MovieNET.Director> listDirector;
        private MovieNET.MovieType selectedMovieType;
        private List<MovieNET.Actor> selectedActors = new List<MovieNET.Actor>();
        private MovieNET.Director selectedDirector;
        private String movieType;
        private String isVisibleAutreType = "Hidden";
        private String directorFirstname;
        private String directorLastname;
        private String isVisibleAutreDirector = "Hidden";
        private String image;
        private String movieTitle;
        private String movieSynopsys;
        private String movieDurationHour = "00";
        private String movieDurationMin = "00";
        private String movieDurationSec = "00";
        private int id_movitype;
        private int id_director;
        private int id_image;
        private int id_movie;
        private String actorFirstname;
        private String actorLastname;
        private MovieNET.User User_co;

        public AddViewModel()
        {
            CommandAdd = new RelayCommand<object>(CommandAddExecute, CommandAddCanExecute);
            CommandAddActor = new RelayCommand(CommandAddActorExecute, CommandAddActorCanExecute);
            listMovieType = serviceClient.SelectAllMovieType();
            listMovieType.Add(new MovieNET.MovieType() { Id_type = 0, Type = "Autre" });
            listDirector = serviceClient.SelectAllDirector();
            ListDirector.Add(new MovieNET.Director() { Id_director = 0, Firstname = "Autre" });
            listActor = serviceClient.SelectAllActor();
            Messenger.Default.Register<MovieNET.User>(this, (user_co) =>
            {
                User_co = user_co;
            });
        }

        public RelayCommand<object> CommandAdd { get; }

        void CommandAddExecute(object parameter)
        {
            var values = (object[])parameter;
            CheckComboBox ccb = (CheckComboBox)values[0];
            Page page = (Page)values[1];
            if (ccb.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ccb.SelectedItems.Count; i++)
                {
                    SelectedActors.Add((MovieNET.Actor)ccb.SelectedItems[i]);
                }
            }
            if (SelectedMovieType.Id_type == 0)
                Id_movitype = serviceClient.CreateMovieType(MovieType);
            if (SelectedDirector.Id_director == 0)
                Id_director = serviceClient.CreateDirector(DirectorFirstname, DirectorLastname);
            Id_image = serviceClient.CreateImage(Image);
            Id_movie = serviceClient.CreateMovie(MovieTitle, MovieSynopsys,
                new TimeSpan(int.Parse(MovieDurationHour), int.Parse(MovieDurationMin),
                int.Parse(MovieDurationSec)), Id_movitype, Id_director, Id_image);
            List<int> id_actors = new List<int>();
            foreach (MovieNET.Actor actor in SelectedActors)
            {
                id_actors.Add(actor.Id_actor);
            }
            serviceClient.AddMovieActors(Id_movie, id_actors);
           
            Messenger.Default.Send(Id_movie);
            Messenger.Default.Send(User_co);
            clearPage();
            var PageW = Window.GetWindow(page);
            PageW.Content = new View.FilmPage();
        }

        bool CommandAddCanExecute(object parameter)
        {
            if (MovieTitle != null && MovieTitle.Length > 0 &&
                     MovieSynopsys != null && MovieSynopsys.Length > 0 &&
                     Image != null && Image.Length > 0 &&
                     MovieDurationHour != null && MovieDurationHour.Length > 0 &&
                     MovieDurationMin != null && MovieDurationMin.Length > 0 &&
                     MovieDurationSec != null && MovieDurationSec.Length > 0 &&
                     ((SelectedMovieType.Id_type != 0 && Id_movitype != 0) ||
                     (MovieType != null && MovieType.Length > 0)) &&
                     ((SelectedDirector.Id_director != 0 && Id_director != 0) ||
                     (DirectorFirstname != null && DirectorFirstname.Length > 0 &&
                     DirectorLastname != null && DirectorLastname.Length > 0)))
                return true;
            else
                return false;
        }

        public RelayCommand CommandAddActor { get; }

        void CommandAddActorExecute()
        {
            bool exist = false;
            foreach (MovieNET.Actor actor in ListActor)
            {
                if (actor.Firstname == ActorFirstname && actor.Lastname == actorLastname)
                    exist = true;
            }
            if (exist == false)
                serviceClient.CreateActor(ActorFirstname, ActorLastname);
            ListActor = serviceClient.SelectAllActor();
            ActorFirstname = "";
            ActorLastname = "";
        }

        bool CommandAddActorCanExecute()
        {
            if (ActorFirstname != null && ActorFirstname.Length > 0 &&
                ActorLastname != null && ActorLastname.Length > 0)
                return true;
            else
                return false;
        }

        void clearPage()
        {
            selectedMovieType = ListMovieType[0];
            SelectedActors.Clear();
            selectedDirector = ListDirector[0];
            movieType = "";
            isVisibleAutreType = "Hidden";
            directorFirstname = "";
            directorLastname = "";
            isVisibleAutreDirector = "Hidden";
            image = "";
            movieTitle = "";
            movieSynopsys = "";
            movieDurationHour = "00";
            movieDurationMin = "00";
            movieDurationSec = "00";
            id_movitype = selectedMovieType.Id_type;
            id_director = selectedDirector.Id_director;
            id_image = 0;
            id_movie = 0;
        }

        public List<MovieNET.MovieType> ListMovieType
        {
            get { return listMovieType; }
            set
            {
                if (value != listMovieType)
                {
                    listMovieType = value;
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

        public List<MovieNET.Director> ListDirector
        {
            get { return listDirector; }
            set
            {
                if (value != listDirector)
                {
                    listDirector = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MovieNET.MovieType SelectedMovieType
        {
            get { return selectedMovieType; }
            set
            {
                if (value != selectedMovieType)
                {
                    selectedMovieType = value;
                    RaisePropertyChanged();
                    if (selectedMovieType.Id_type == 0)
                        IsVisibleAutreType = "Visible";
                    else
                    {
                        IsVisibleAutreType = "Hidden";
                        Id_movitype = selectedMovieType.Id_type;
                    }
                }
            }
        }

        public List<MovieNET.Actor> SelectedActors
        {
            get { return selectedActors; }
            set
            {
                if (value != selectedActors)
                {
                    selectedActors = value;
                    RaisePropertyChanged();

                }
            }
        }

        public MovieNET.Director SelectedDirector
        {
            get { return selectedDirector; }
            set
            {
                if (value != selectedDirector)
                {
                    selectedDirector = value;
                    RaisePropertyChanged();
                    if (selectedDirector.Id_director == 0)
                        IsVisibleAutreDirector = "Visible";
                    else
                    {
                        IsVisibleAutreDirector = "Hidden";
                        Id_director = SelectedDirector.Id_director;
                    }
                }
            }
        }

        public string MovieType
        {
            get { return movieType; }
            set
            {
                if (value != movieType)
                {
                    movieType = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String IsVisibleAutreType
        {
            get { return isVisibleAutreType; }
            set
            {
                if (value != isVisibleAutreType)
                {
                    isVisibleAutreType = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String DirectorFirstname
        {
            get { return directorFirstname; }
            set
            {
                if (value != directorFirstname)
                {
                    directorFirstname = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String DirectorLastname
        {
            get { return directorLastname; }
            set
            {
                if (value != directorLastname)
                {
                    directorLastname = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String IsVisibleAutreDirector
        {
            get { return isVisibleAutreDirector; }
            set
            {
                if (value != isVisibleAutreDirector)
                {
                    isVisibleAutreDirector = value;
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

        public String MovieTitle
        {
            get { return movieTitle; }
            set
            {
                if (value != movieTitle)
                {
                    movieTitle = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String MovieSynopsys
        {
            get { return movieSynopsys; }
            set
            {
                if (value != movieSynopsys)
                {
                    movieSynopsys = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String MovieDurationHour
        {
            get { return movieDurationHour; }
            set
            {
                if (value != movieDurationHour)
                {
                    int h;
                    movieDurationHour = value;
                    RaisePropertyChanged();
                    if (int.TryParse(movieDurationHour, out h) == false || movieDurationHour == null ||
                        movieDurationHour.Length <= 0 || int.Parse(movieDurationHour) >= 24 ||
                        int.Parse(movieDurationHour) <= 0)
                        movieDurationHour = "00";
                }

            }
        }

        public String MovieDurationMin
        {
            get { return movieDurationMin; }
            set
            {
                if (value != movieDurationMin)
                {
                    int min;
                    movieDurationMin = value;
                    RaisePropertyChanged();
                    if (int.TryParse(MovieDurationMin, out min) == false || movieDurationMin == null ||
                        movieDurationMin.Length <= 0 || int.Parse(movieDurationMin) >= 60 ||
                        int.Parse(movieDurationMin) <= 0)
                        movieDurationMin = "00";
                }
            }
        }

        public String MovieDurationSec
        {
            get { return movieDurationSec; }
            set
            {
                if (value != movieDurationSec)
                {
                    int sec;
                    movieDurationSec = value;
                    RaisePropertyChanged();
                    if (int.TryParse(MovieDurationSec, out sec) == false || movieDurationSec == null ||
                        movieDurationSec.Length <= 0 || int.Parse(movieDurationSec) >= 60 ||
                        int.Parse(movieDurationSec) <= 0)
                        movieDurationSec = "00";
                }
            }
        }

        public int Id_movitype
        {
            get { return id_movitype; }
            set
            {
                if (value != id_movitype)
                {
                    id_movitype = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int Id_director
        {
            get { return id_director; }
            set
            {
                if (value != id_director)
                {
                    id_director = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int Id_image
        {
            get { return id_image; }
            set
            {
                if (value != id_image)
                {
                    id_image = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int Id_movie
        {
            get { return id_movie; }
            set
            {
                if (value != id_movie)
                {
                    id_movie = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String ActorFirstname
        {
            get { return actorFirstname; }
            set
            {
                if (value != actorFirstname)
                {
                    actorFirstname = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String ActorLastname
        {
            get { return actorLastname; }
            set
            {
                if (value != actorLastname)
                {
                    actorLastname = value;
                    RaisePropertyChanged();
                }
            }
        }

    }
}