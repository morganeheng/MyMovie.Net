using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace MovieNetWpf.ViewModel
{
    class MenuViewModel : ViewModelBase
    {
        private String msg;
        private View.PageWindow PageW = new View.PageWindow();
        private MovieNET.User User_co;

        public MenuViewModel()
        {
            CommandLogout = new RelayCommand<Window>(CommandLogoutExecute);
            CommandList = new RelayCommand(CommandListExecute);
            CommandAdd = new RelayCommand(CommandAddExecute);
            CommandDelete = new RelayCommand(CommandDeleteExecute);
            CommandUpdate = new RelayCommand(CommandUpdateExecute);
            CommandSearch = new RelayCommand(CommandSearchExecute);
            CommandProfil = new RelayCommand(CommandProfilExecute);
            OnClosingCommand = new RelayCommand(OnClosingCommandExecute);
            Messenger.Default.Register<MovieNET.User>(this, (user_co) =>
            {
                User_co = user_co;
                Msg = "\nBonjour " + User_co.Login + ",\n\nBienvenue dans \nl'application \nMovie Net";
            });
        }

        public string Msg
        {
            get { return msg; }
            set
            {
                if (value != msg)
                {
                    msg = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand<Window> CommandLogout { get; }

        void CommandLogoutExecute(Window window)
        {
            window.Close();
        }

        public RelayCommand CommandList { get; }

        void CommandListExecute()
        {
            if (this.PageW.Activate() == false)
                PageW = new View.PageWindow();
            PageW.Content = new View.ListPage();
            Messenger.Default.Send(User_co);
            PageW.Show();
        }

        public RelayCommand CommandAdd { get; }

        void CommandAddExecute()
        {
            if (this.PageW.Activate() == false)
                PageW = new View.PageWindow();
            PageW.Content = new View.AddPage();
            Messenger.Default.Send(User_co);
            PageW.Show();
        }

        public RelayCommand CommandDelete { get; }

        void CommandDeleteExecute()
        {
            if (this.PageW.Activate() == false)
                PageW = new View.PageWindow();
            PageW.Content = new View.DeletePage();
            Messenger.Default.Send(User_co);
            PageW.Show();
        }

        public RelayCommand CommandUpdate { get; }

        void CommandUpdateExecute()
        {
            if (this.PageW.Activate() == false)
                PageW = new View.PageWindow();
            PageW.Content = new View.UpdatePage();
            Messenger.Default.Send(User_co);
            PageW.Show();
        }

        public RelayCommand CommandSearch { get; }

        void CommandSearchExecute()
        {
            if (this.PageW.Activate() == false)
                PageW = new View.PageWindow();
            PageW.Content = new View.SearchPage();
            Messenger.Default.Send(User_co);
            PageW.Show();
        }

        public RelayCommand CommandProfil { get; }

        void CommandProfilExecute()
        {
            if (this.PageW.Activate() == false)
                PageW = new View.PageWindow();
            PageW.Content = new View.ProfilPage();
            Messenger.Default.Send(User_co);
            PageW.Show();
        }

        public RelayCommand OnClosingCommand { get; }

        void OnClosingCommandExecute()
        {
            MainWindow MainW = new MainWindow();
            MainW.Show();
            PageW.Close();
        }
    }
}