using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MovieNetWpf.ServiceFacade;
using System.Windows;

namespace MovieNetWpf.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private FacadeClient serviceClient = new FacadeClient();
        private String user;
        private String password;

        public MainViewModel()
        {
            CommandRegister = new RelayCommand<Window>(CommandRegisterExecute);
            CommandConnect = new RelayCommand<Window>(CommandConnectExecute);
            OnClosingCommand = new RelayCommand(OnClosingCommandExecute);
        }

        public string User
        {
            get { return user; }
            set
            {
                if (value != user)
                {
                    user = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (value != password)
                {
                    password = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand<Window> CommandRegister { get; }

        void CommandRegisterExecute(Window window)
        {
            if (User != null && Password != null && User.Length > 0 && Password.Length > 0)
            {
                if (serviceClient.CheckUserExist(User))
                    MessageBox.Show("Ce LOGIN existe déjà!");
                else
                {
                    serviceClient.CreateUser(User, Password);
                    MessageBox.Show("Inscription réussie");
                    View.MenuWindow MenuW = new View.MenuWindow();
                    MenuW.Show();
                    int Id_User = serviceClient.SelectIdByLogin(User);
                    MovieNET.User user_co = serviceClient.SelectOneUser(Id_User);
                    Messenger.Default.Send(user_co);
                    window.Hide();
                }
            }
            else
            {
                if (User == null || User.Length <= 0)
                    MessageBox.Show("Le champs LOGIN ne doit pas être vide!");
                if (Password == null || Password.Length <= 0)
                    MessageBox.Show("Le champs PASSWORD ne doit pas être vide!");
            }
        }

        public RelayCommand<Window> CommandConnect { get; }

        void CommandConnectExecute(Window window)
        {
            if (User != null && Password != null && User.Length > 0 && Password.Length > 0)
            {
                if (serviceClient.CheckUser(User, Password))
                {
                    MessageBox.Show("Connexion réussie");
                    View.MenuWindow MenuW = new View.MenuWindow();
                    MenuW.Show();
                    int Id_User = serviceClient.SelectIdByLogin(User);
                    MovieNET.User user_co = serviceClient.SelectOneUser(Id_User);
                    Messenger.Default.Send(user_co);
                    window.Hide();
                }
                else
                {
                    if (serviceClient.CheckUserExist(User))
                        MessageBox.Show("Mot de passe erroné!");
                    else
                        MessageBox.Show("Ce LOGIN n'existe pas!");
                }
            }
            else
            {
                if (User == null || User.Length <= 0)
                    MessageBox.Show("Le champs LOGIN ne doit pas être vide!");
                if (Password == null || Password.Length <= 0)
                    MessageBox.Show("Le champs PASSWORD ne doit pas être vide!");
            }
        }

        public RelayCommand OnClosingCommand { get; }

        void OnClosingCommandExecute()
        {
            App.Current.Shutdown();
        }
    }
}