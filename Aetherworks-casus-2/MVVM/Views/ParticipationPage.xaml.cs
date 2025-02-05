using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class ParticipationPage : ContentPage
{
	public ParticipationPage(LocalDbService dbService)
	{
		InitializeComponent();
		BindingContext = new ParticipationViewModel(dbService);
	}
}