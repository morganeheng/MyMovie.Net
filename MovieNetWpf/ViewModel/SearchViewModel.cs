using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MovieNetWpf.ServiceFacade;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MovieNetWpf.ViewModel
{
    class SearchViewModel : ViewModelBase
    {
        private FacadeClient serviceClient = new FacadeClient();
        private List<MovieNET.Movie> listMovie;
        private MovieNET.Movie selectedMovie;
        private String listTitle = "Liste des films : ";
        private String movieTitle;
        private String movieType;
        private int ID_movie;
        private MovieNET.User User_co;

        public SearchViewModel()
        {
            GoMovie = new RelayCommand<Page>(GoMovieExecute);
            CommandSearchByTitle = new RelayCommand(CommandSearchByTitleExecute, CommandSearchByTitleCanExecute);
            CommandSearchByType = new RelayCommand(CommandSearchByTypeExecute, CommandSearchByTypeCanExecute);
            Messenger.Default.Register<MovieNET.User>(this, (user_co) =>
            {
                ListMovie = serviceClient.SelectAllMovie();
                User_co = user_co;
            });
        }

        public RelayCommand<Page> GoMovie { get; }

        void GoMovieExecute(Page page)
        {
            if (selectedMovie != null)
            {
                ID_movie = selectedMovie.Id_movie;
                Messenger.Default.Send(ID_movie);
                Messenger.Default.Send(User_co);
                clearPage();
                var PageW = Window.GetWindow(page);
                PageW.Content = new View.FilmPage();
            }
        }

        public RelayCommand CommandSearchByTitle { get; }

        void CommandSearchByTitleExecute()
        {
            ListMovie = serviceClient.SelectMovieByTitle(MovieTitle);
            ListTitle = "Liste des films qui contient \"" + MovieTitle + "\" : ";
            MovieType = "";
        }

        bool CommandSearchByTitleCanExecute()
        {
            if (MovieTitle != null && MovieTitle.Length > 0)
                return true;
            else
                return false;
        }

        public RelayCommand CommandSearchByType { get; }

        void CommandSearchByTypeExecute()
        {
            ListMovie = serviceClient.SelectMovieByMovieType(MovieType);
            ListTitle = "Liste des films du type \"" + MovieType + "\" : ";
            MovieTitle = "";
        }

        bool CommandSearchByTypeCanExecute()
        {
            if (MovieType != null && MovieType.Length > 0)
                return true;
            else
                return false;
        }

        void clearPage()
        {
            ListMovie = serviceClient.SelectAllMovie(); ;
            ListTitle = "Liste des films : ";
            MovieTitle = "";
            MovieType = "";
        }

        public List<MovieNET.Movie> ListMovie
        {
            get { return listMovie; }
            set
            {
                if (value != listMovie)
                {
                    listMovie = value;
                    RaisePropertyChanged();
                }
            }
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

        public String ListTitle
        {
            get { return listTitle; }
            set
            {
                if (value != listTitle)
                {
                    listTitle = value;
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
                    ListMovie = serviceClient.SelectAllMovie();
                    ListTitle = "Liste des films : ";
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
                    if (MovieTitle.Length <= 0 && MovieType.Length <= 0)
                    {
                        ListMovie = serviceClient.SelectAllMovie();
                        ListTitle = "Liste des films : ";
                    }
                }
            }
        }
    }
}
