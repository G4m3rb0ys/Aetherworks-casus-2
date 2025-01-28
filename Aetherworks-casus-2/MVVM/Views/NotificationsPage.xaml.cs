using Plugin.LocalNotification;


namespace Aetherworks_casus_2.MVVM.Views;

public partial class NotificationsPage : ContentPage
{
	public NotificationsPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var request = new NotificationRequest
		{
			NotificationId = 1337,
			Title = "New Activity!",
			Subtitle = "Bonjours",
			Description = "This is a description",
			BadgeNumber = 42,
			Schedule = new NotificationRequestSchedule
			{
				NotifyTime = DateTime.Now.AddSeconds(5)
			}
		};

        await LocalNotificationCenter.Current.Show(request);
    }
}