using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class SuggestionPage : ContentPage
{
	public SuggestionPage(LocalDbService dbService)
	{
		InitializeComponent();
		BindingContext = new SuggestionViewModel(dbService);
	}
}