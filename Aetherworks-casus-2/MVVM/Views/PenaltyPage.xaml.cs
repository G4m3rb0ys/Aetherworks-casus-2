using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class PenaltyPage : ContentPage
{
	public PenaltyPage(LocalDbService dbService)
	{
		InitializeComponent();
		BindingContext = new PenaltyViewModel(dbService);
	}
}