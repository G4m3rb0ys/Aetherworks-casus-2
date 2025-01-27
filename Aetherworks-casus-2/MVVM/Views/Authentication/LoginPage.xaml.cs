using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;


namespace Aetherworks_casus_2.MVVM.Views.Authentication;

public partial class LoginPage : ContentPage
{
    private readonly LocalDbService _db;
    public LoginPage(LocalDbService db)
    {
        InitializeComponent();
        _db = db;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(EmailOrUsernameEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        User loggedInUser = await _db.GetUser(EmailOrUsernameEntry.Text);
        if (loggedInUser == null)
        {
            DisplayAlert("Error", "User not found", "OK");
            return;
        }
        SessionService.LoggedInUser = loggedInUser;
        App.Current.MainPage = new NavigationPage(new Navigationbar());
    }
}