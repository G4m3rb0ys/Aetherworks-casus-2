using Aetherworks_casus_2.MVVM.ViewModels;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class UserPage : ContentPage
{
	public UserPage(LocalDbService dbService)
	{
		InitializeComponent();
		BindingContext = new UserViewModel(dbService);
	}
}