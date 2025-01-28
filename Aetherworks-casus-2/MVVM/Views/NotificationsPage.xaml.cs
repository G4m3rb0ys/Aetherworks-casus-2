using Aetherworks_casus_2.MVVM.ViewModels;
using Plugin.LocalNotification;


namespace Aetherworks_casus_2.MVVM.Views;

public partial class NotificationsPage : ContentPage
{
	private NotificationsViewModel viewModel = new NotificationsViewModel();
	public NotificationsPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		viewModel.ShowNotification();
    }
}