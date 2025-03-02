using RESTNataliaFdez.MVVM.ViewModels;
using RESTNataliaFdez.MVVM.Views;

namespace RESTNataliaFdez
{
    public partial class App : Application
    {
        public static DataViewModel SharedViewModel { get; set; }
        public App()
        {
            InitializeComponent();
            SharedViewModel = new DataViewModel();
            MainPage = new NavigationPage (new MainView());
        }
    }
}
