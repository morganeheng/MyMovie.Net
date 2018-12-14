using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MovieNetWpf.ServiceFacade;
using System.Windows;
using System.Windows.Controls;

namespace MovieNetWpf.ViewModel
{
    class ProfilViewModel : ViewModelBase
    {
        private string user_name;
        private string oldpass;
        private string newpass;
        private MovieNET.User User_co;
        private FacadeClient serviceClient = new FacadeClient();

        public ProfilViewModel()
        {
            CommandUpdatePass = new RelayCommand(CommandUpdatePassExecute);
            CommandDeleteUser = new RelayCommand<Page>(CommandDeleteUserExecute);
            Messenger.Default.Register<MovieNET.User>(this, (user_co) =>
            {
                User_co = user_co;
                MovieNET.User user = serviceClient.SelectOneUser(User_co.Id_user);
                user_name = User_co.Login;
            });
        }

        public String UserName
        {
            get { return user_name; }
            set
            {
                if (value != user_name)
                {
                    user_name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string OldPass
        {
            get { return oldpass; }
            set
            {
                if (value != oldpass)
                {
                    oldpass = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string NewPass
        {
            get { return newpass; }
            set
            {
                if (value != newpass)
                {
                    newpass = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand CommandUpdatePass { get; }

        void CommandUpdatePassExecute()
        {
            MovieNET.User user = serviceClient.SelectOneUser(User_co.Id_user);
            if (OldPass == user.Password)
            {
                serviceClient.ModifyUser(User_co.Id_user, user.Login, NewPass);
                MessageBox.Show("Votre mot de passe a été modifié");
                OldPass = "";
                NewPass = "";
            }
            else
            {
                MessageBox.Show("Le mot de passe actuel est incorrect");
            }
        }

        public RelayCommand<Page> CommandDeleteUser { get; }

        void CommandDeleteUserExecute(Page page)
        {
            MessageBoxResult dialogResult = MessageBox.Show("", "Voulez-vous supprimer votre compte ? L'application se fermera", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                serviceClient.DeleteUser(User_co.Id_user);
                var PageW = Window.GetWindow(page);
                PageW.Close();
                App.Current.Shutdown();
            }
        }
    }
}