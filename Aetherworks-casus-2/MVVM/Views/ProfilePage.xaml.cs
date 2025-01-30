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

        private async void OnGenerateQRCodeClicked(object sender, EventArgs e)
        {
            if (ProfileView.LoggedInUser != null)
            {
                await Navigation.PushAsync(new QRCodeGeneratorPage(ProfileView.LoggedInUser.Id.ToString(), "User"));
            }
        }

        private void OnLogOutClicked(object sender, EventArgs e)
        {
            ProfileView.LogOut();
            App.Current.MainPage = new NavigationPage(new StartPage(_db));
        }
        
    }
}