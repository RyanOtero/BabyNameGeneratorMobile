using NameGeneratorMobile.ViewModel;
using Xamarin.Forms;

namespace NameGeneratorMobile {
    public partial class App : Application {

        MainViewModel mainViewModel;
        public App() {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override async void OnStart() {
            mainViewModel = (MainViewModel)MainPage.BindingContext;
            await mainViewModel.nameGenerator.GetNamesAsync();
        }

        protected override void OnSleep() {
            mainViewModel.Stash();
        }

        protected override void OnResume() {
        }
    }
}
