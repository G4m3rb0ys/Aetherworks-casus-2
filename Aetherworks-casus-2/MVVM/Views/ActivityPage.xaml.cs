using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class ActivityPage : ContentPage
{
    private readonly VictuzActivity _activity;
    private readonly LocalDbService _dbService;

    public ActivityPage(VictuzActivity activity, LocalDbService dbService)
    {
        InitializeComponent();
        BindingContext = this;

        _activity = activity;
        _dbService = dbService;

        LoadActivityDetails();
    }

    private void LoadActivityDetails()
    {
        ActivityNameLabel.Text = _activity.Name;
        ActivityDescriptionLabel.Text = _activity.Description;
        ActivityDateTimeLabel.Text = $"Date & Time: {_activity.ActivityDate:dd MMM yyyy, HH:mm}";
        ActivityLocationLabel.Text = $"Location: {_activity.Location?.Name ?? "Unknown"}";

        if (!string.IsNullOrWhiteSpace(_activity.Picture))
        {
            ActivityImage.Source = ImageSource.FromFile(_activity.Picture);
            ActivityImage.IsVisible = true;
        }
        else
        {
            ActivityImage.IsVisible = false;
        }

        int remainingSpots = _activity.ParticipationLimit - (_activity.Participations?.Count ?? 0);
        if (remainingSpots <= 0)
        {
            ActivityAvailabilityLabel.Text = "This activity is fully booked.";
            ActivityAvailabilityLabel.TextColor = Colors.Red;
            SignUpButton.IsEnabled = false;
        }
        else
        {
            ActivityAvailabilityLabel.Text = $"{remainingSpots} spots left.";
            ActivityAvailabilityLabel.TextColor = Colors.Green;
            SignUpButton.IsEnabled = true;
        }
    }

    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Signed Up", "You have successfully signed up for this activity!", "OK");

        _activity.Participations ??= new List<Participation>();
        _activity.Participations.Add(new Participation
        {
            UserId = 1,
            ActivityId = _activity.Id,
            Attend = false
        });

        LoadActivityDetails();

        _dbService.UpdateActivity(_activity);
    }
}
