using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class ActivityPage : ContentPage
{
	public ActivityPage(LocalDbService dbService)
	{
		InitializeComponent();
        BindingContext = new ActivityViewModel(dbService);
    }
}