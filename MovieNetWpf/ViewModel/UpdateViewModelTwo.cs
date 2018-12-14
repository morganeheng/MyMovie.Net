using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MovieNetWpf.ServiceFacade;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace MovieNetWpf.ViewModel
{
    class UpdateViewModelTwo : ViewModelBase
    {
        private FacadeClient serviceClient = new FacadeClient();
        private int id_movie;
        private MovieNET.Movie selectedMovie;
        private String movieTitle;
        private String movieSynopsys;
        private String movieDurationHour;
        private String movieDurationMin;
        private String movieDurationSec;
        private MovieNET.MovieType selectedMovieType;
        private String movieType;
        private MovieNET.Director selectedDirector;
        private String movieDirectorFirstname;
        private String movieDirectorLastname;
        private MovieNET.Image selectedImage;
        private String image;
        private List<MovieNET.Actor> selectedActors = new List<MovieNET.Actor>();
        private List<MovieNET.Actor> listActorMovie;
        private List<MovieNET.MovieType> listMovieType;
        private List<MovieNET.Director> listDirector;
        private List<MovieNET.Actor> listActor;
        private List<int> id_actors = new List<int>();
        private String isVisibleAutreType = "Hidden";
        private String isVisibleAutreDirector = "Hidden";
        private int id_movitype;
        private int id_director;
        private String actorFirstname;
        private String actorLastname;
        private int movieTypeIndex = 0;
        private int directorIndex = 0;
        private MovieNET.User user_co;


        public UpdateViewModelTwo()
        {
            CommandUpdate = new RelayCommand<object>(CommandUpdateExecute, CommandUpdateCanExecute);
            CommandAddActor = new RelayCommand(CommandAddActorExecute, CommandAddActorCanExecute);
            CommandReturn = new RelayCommand<Page>(CommandReturnExecute);
            Messenger.Default.Register<MovieNET.User>(this, (User_co) =>
            {
                user_co = User_co;
            });
            Messenger.Default.Register<int>(this, (ID_movie) =>
            {
                id_movie = ID_movie;
                selectedMovie = serviceClient.SelectOneMovie(id_movie);
                if (selectedMovie != null)
                {
                    SelectedMovieType = serviceClient.SelectOneMovieType(selectedMovie.Id_type);
                    SelectedDirector = serviceClient.SelectOneDirector(selectedMovie.Id_director);
                    SelectedImage = serviceClient.SelectOneImage(selectedMovie.Id_image);
                    ListActorMovie = serviceClient.SelectActorByMovie(selectedMovie.Id_movie);

                    MovieTitle = selectedMovie.Title;
                    MovieSynopsys = selectedMovie.Synopsis;
                    MovieDurationHour = selectedMovie.Duration.Hours.ToString();
                    MovieDurationMin = selectedMovie.Duration.Minutes.ToString();
                    MovieDurationSec = selectedMovie.Duration.Seconds.ToString();
                    id_director = SelectedDirector.Id_director;
                    id_movitype = SelectedMovieType.Id_type;
                    Image = SelectedImage.URL;
                    listMovieType = serviceClient.SelectAllMovieType();
                    listMovieType.Add(new MovieNET.MovieType() { Id_type = 0, Type = "Autre" });
                    listDirector = serviceClient.SelectAllDirector();
                    ListDirector.Add(new MovieNET.Director() { Id_director = 0, Firstname = "Autre" });
                    listActor = serviceClient.SelectAllActor();
                    for (int i = 0; i < listMovieType.Count; i++)
                    {
                        if (listMovieType[i].Id_type == SelectedMovieType.Id_type)
                            MovieTypeIndex = i;
                    }
                    for (int i = 0; i < listDirector.Count; i++)
                    {
                        if (listDirector[i].Id_director == SelectedDirector.Id_director)
                            DirectorIndex = i;
                    }
                }
            });
        }


        public RelayCommand<object> CommandUpdate { get; }

        void CommandUpdateExecute(object parameter)
        {
            var values = (object[])parameter;
            CheckComboBox ccb = (CheckComboBox)values[0];
            Page page = (Page)values[1];
            foreach (MovieNET.Actor actor in listActorMovie)
            {
                id_actors.Add(actor.Id_actor);
            }
            serviceClient.DeleteMovieActors(selectedMovie.Id_movie, id_actors);
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
                Id_director = serviceClient.CreateDirector(MovieDirectorFirstname, MovieDirectorLastname);
            serviceClient.ModifyImage(selectedMovie.Id_image, Image);
            serviceClient.ModifyMovie(selectedMovie.Id_movie, MovieTitle, MovieSynopsys,
                                      new TimeSpan(int.Parse(MovieDurationHour), int.Parse(MovieDurationMin),
                                      int.Parse(MovieDurationSec)), Id_movitype, Id_director,
                                      selectedMovie.Id_image);
            id_actors.Clear();
            foreach (MovieNET.Actor actor in SelectedActors)
            {
                id_actors.Add(actor.Id_actor);
            }
            serviceClient.AddMovieActors(id_movie, id_actors);
            clearPage();
            Messenger.Default.Send(id_movie);
            Messenger.Default.Send(user_co);
            var PageW = Window.GetWindow(page);
            PageW.Content = new View.FilmPage();
        }

        bool CommandUpdateCanExecute(object parameter)
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
                     (MovieDirectorFirstname != null && MovieDirectorFirstname.Length > 0 &&
                     MovieDirectorLastname != null && MovieDirectorLastname.Length > 0)))
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

        public RelayCommand<Page> CommandReturn { get; }

        void CommandReturnExecute(Page page)
        {
            Messenger.Default.Send(user_co);
            var PageW = Window.GetWindow(page);
            PageW.Content = new View.UpdatePage();
        }

        void clearPage()
        {
            SelectedActors.Clear();
            movieType = "";
            isVisibleAutreType = "Hidden";
            movieDirectorFirstname = "";
            movieDirectorLastname = "";
            isVisibleAutreDirector = "Hidden";
        }

        public MovieNET.Movie SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                if (value != selectedMovie)
                {
                    selectedMovie = value;
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
        public MovieNET.Image SelectedImage
        {
            get { return selectedImage; }
            set
            {
                if (value != selectedImage)
                {
                    selectedImage = value;
                    RaisePropertyChanged();
                }
            }
        }
        public List<MovieNET.Actor> ListActorMovie
        {
            get { return listActorMovie; }
            set
            {
                if (value != listActorMovie)
                {
                    listActorMovie = value;
                    RaisePropertyChanged();
                }
            }
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

        public String MovieDirectorFirstname
        {
            get { return movieDirectorFirstname; }
            set
            {
                if (value != movieDirectorFirstname)
                {
                    movieDirectorFirstname = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String MovieDirectorLastname
        {
            get { return movieDirectorLastname; }
            set
            {
                if (value != movieDirectorLastname)
                {
                    movieDirectorLastname = value;
                    RaisePropertyChanged();
                }
            }
        }

        public String MovieType
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

        public int MovieTypeIndex
        {
            get { return movieTypeIndex; }
            set
            {
                if (value != movieTypeIndex)
                {
                    movieTypeIndex = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int DirectorIndex
        {
            get { return directorIndex; }
            set
            {
                if (value != directorIndex)
                {
                    directorIndex = value;
                    RaisePropertyChanged();
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
    }
}