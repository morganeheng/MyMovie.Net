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
    class DeleteViewModel : ViewModelBase
    {
        private FacadeClient serviceClient = new FacadeClient();
        private List<MovieNET.Movie> listMovie;
        private MovieNET.Movie selectedMovie;
        private String listTitle = "Liste des films : ";
        private String movieTitle;
        private String movieType;

        public DeleteViewModel()
        {
            GoDeleteMovie = new RelayCommand<Page>(GoDeleteMovieExecute);
            CommandSearchByTitle = new RelayCommand(CommandSearchByTitleExecute, CommandSearchByTitleCanExecute);
            CommandSearchByType = new RelayCommand(CommandSearchByTypeExecute, CommandSearchByTypeCanExecute);
            Messenger.Default.Register<MovieNET.User>(this, (user_co) =>
            {
                ListMovie = serviceClient.SelectAllMovie();
            });
        }

        public RelayCommand<Page> GoDeleteMovie { get; }

        void GoDeleteMovieExecute(Page page)
        {
            if (selectedMovie != null)
            {
                MessageBoxResult dialogResult = MessageBox.Show("", "Voulez-vous supprimer ce film ?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    serviceClient.DeleteMovie(selectedMovie.Id_movie);
                    serviceClient.DeleteImage(selectedMovie.Id_image);
                    MessageBox.Show("Le film a été supprimé");
                    ListMovie = serviceClient.SelectAllMovie();
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    MessageBox.Show("La suppression du film a été annulé");
                }
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