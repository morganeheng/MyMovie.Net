namespace MovieNetWpf.ViewModel
{
    class ViewModelLocator
    {
        public static MainViewModel MainVM { get; } = new MainViewModel();
        public static MenuViewModel MenuVM { get; } = new MenuViewModel();
        public static ListViewModel ListVM { get; } = new ListViewModel();
        public static AddViewModel AddVM { get; } = new AddViewModel();
        public static DeleteViewModel DeleteVM { get; } = new DeleteViewModel();
        public static UpdateViewModel UpdateVM { get; } = new UpdateViewModel();
        public static UpdateViewModelTwo UpdateVMTwo { get; } = new UpdateViewModelTwo();
        public static SearchViewModel SearchVM { get; } = new SearchViewModel();
        public static ProfilViewModel ProfilVM { get; } = new ProfilViewModel();
        public static FilmViewModel FilmVM { get; } = new FilmViewModel();
    }
}