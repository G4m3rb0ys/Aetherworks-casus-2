using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;


namespace Aetherworks_casus_2.MVVM.Views.Authentication;
public partial class StartPage : ContentPage
{
    private readonly LocalDbService _db;
    public StartPage(LocalDbService db)
	{
		InitializeComponent();
        _db = db;
        //Session.LoggedInUser = null;
    }

	public void OnLoginClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LoginPage(_db));
    }
	public void OnRegisterClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage(_db));
    }

}
