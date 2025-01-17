using Aetherworks_casus_2.MVVM.ViewModels;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class LocationPage : ContentPage
{
	public LocationPage(LocalDbService dbService)
	{
		InitializeComponent();
		BindingContext = new LocationViewModel(dbService);
	}
}