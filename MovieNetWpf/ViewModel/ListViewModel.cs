using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MovieNetWpf.ServiceFacade;
using System.Windows.Controls;
using System.Windows;

namespace MovieNetWpf.ViewModel
{
    class ListViewModel : ViewModelBase
    {
        private FacadeClient serviceClient = new FacadeClient();
        private List<MovieNET.Movie> listMovie;
        private MovieNET.Movie selectedMovie;
        private int ID_movie;
        private MovieNET.User User_co;

        public ListViewModel()
        {
            CommandViewMovie = new RelayCommand<Page>(CommandViewMovieExecute);
            Messenger.Default.Register<MovieNET.User>(this, (user_co) =>
            {
                ListMovie = serviceClient.SelectAllMovie();
                User_co = user_co;
            });
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

        public RelayCommand<Page> CommandViewMovie { get; }

        void CommandViewMovieExecute(Page page)
        {
            var PageW = Window.GetWindow(page);
            PageW.Content = new View.FilmPage();
            ID_movie = selectedMovie.Id_movie;
            Messenger.Default.Send(ID_movie);
            Messenger.Default.Send(User_co);
            PageW.Show();
        }
    }
}