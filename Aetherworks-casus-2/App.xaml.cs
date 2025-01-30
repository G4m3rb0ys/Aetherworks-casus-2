using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Views.Authentication;
using Aetherworks_casus_2.MVVM.Views;

namespace Aetherworks_casus_2
{
    public partial class App : Application
    {
        public App(LocalDbService _db)
        {
            InitializeComponent();

            MainPage = new NavigationPage();

            AutoLogin(_db);
        }

        public async void AutoLogin(LocalDbService _db)
        {
            var userId = await SecureStorage.Default.GetAsync("rememberUserLogin");

            if (userId != null)
            {
                var logInUser = await _db.GetUser(Int32.Parse(userId));
                if (logInUser != null)
                {
                    SessionService.LogIn(logInUser);
                    MainPage = new NavigationPage(new Navigationbar());
                    return;
                }
                else
                {
                    MainPage = new NavigationPage(new StartPage(_db));
                    return;
                }
            }
            else
            {
                MainPage = new NavigationPage(new StartPage(_db));
                return;
            }
        }
    }
}