using Aetherworks_casus_2.MVVM.Views.Authentication;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.ViewModels;
using SQLite;

namespace Aetherworks_casus_2.MVVM.Views
{
    public partial class ProfilePage : ContentPage
    {
        private LocalDbService _db = new LocalDbService();
        private ProfileViewModel ProfileView = new ProfileViewModel();

        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = ProfileView;
        }

        private void OnLogOutClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new StartPage(_db));
        }
        
    }
}