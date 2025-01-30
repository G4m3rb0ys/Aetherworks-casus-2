using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class ActivityPage : ContentPage
{
    private readonly VictuzActivity _activity;
    private readonly LocalDbService _dbService;
    private readonly bool _isAdmin;

    public ActivityPage(VictuzActivity activity, LocalDbService dbService)
    {
        InitializeComponent();
        BindingContext = this;

        _activity = activity;
        _dbService = dbService;
        _isAdmin = SessionService.LoggedInUser?.IsAdmin ?? false;

        if (_isAdmin)
        {
            GenerateQRCodeButton.IsVisible = true;
        }

        LoadActivityDetailsAsync();
    }

    private async void LoadActivityDetailsAsync()
    {
        ActivityNameLabel.Text = _activity.Name;
        ActivityDescriptionLabel.Text = _activity.Description;
        ActivityDateTimeLabel.Text = $"Date & Time: {_activity.ActivityDate:dd MMM yyyy, HH:mm}";

        var location = await _dbService.GetLocation(_activity.LocationId);
        ActivityLocationLabel.Text = $"Location: {location?.Name ?? "Unknown"}";

        var participations = await _dbService.GetParticipations(_activity.Id);
        int remainingSpots = _activity.ParticipationLimit - (participations?.Count ?? 0);

        ActivityAvailabilityLabel.Text = remainingSpots <= 0
            ? "This activity is fully booked."
            : $"{remainingSpots} spots left.";
        ActivityAvailabilityLabel.TextColor = remainingSpots <= 0 ? Colors.Red : Colors.Green;

        SignUpButton.IsEnabled = remainingSpots > 0;
    }

    private async void OnGenerateQRCodeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QRCodeGeneratorPage(_activity.Id.ToString(), "Activity"));
    }

    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Signed Up", "You have successfully signed up for this activity!", "OK");

        var participation = new Participation
        {
            UserId = SessionService.LoggedInUser?.Id ?? 0,
            ActivityId = _activity.Id,
            Attend = false
        };

        await _dbService.AddOrUpdateParticipation(participation);
        await _dbService.AddOrUpdateActivity(_activity);

        LoadActivityDetailsAsync();
    }
}
