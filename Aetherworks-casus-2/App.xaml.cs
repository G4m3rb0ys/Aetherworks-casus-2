using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Views.Authentication;

namespace Aetherworks_casus_2
{
    public partial class App : Application
    {
        public App(LocalDbService _db)
        {
            InitializeComponent();
            MainPage = MainPage = new NavigationPage(new StartPage(_db));
        }
    }
}