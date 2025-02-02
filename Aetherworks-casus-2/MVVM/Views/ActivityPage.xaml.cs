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
            ScanAttendanceButton.IsVisible = true;
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

        if (!string.IsNullOrWhiteSpace(_activity.Picture))
        {
            ActivityImage.Source = ImageSource.FromFile(_activity.Picture);
            ActivityImage.IsVisible = true;
        }
        else
        {
            ActivityImage.IsVisible = false;
        }

        var participations = await _dbService.GetParticipations(_activity.Id);
        int remainingSpots = _activity.ParticipationLimit - (participations?.Count ?? 0);
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
        }

        bool userSignedUp = await IsUserSignedUp();
        SignUpButton.IsVisible = !userSignedUp;
        SignOutButton.IsVisible = userSignedUp;
    }


    private async void OnGenerateQRCodeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QRCodeGeneratorPage(_activity.Id.ToString(), "Activity"));
    }

    private async void OnScanQRCodeButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QRscanPage(_activity.Id));
    }


    private async Task<bool> IsUserSignedUp()
    {
        var participations = await _dbService.GetParticipations(_activity.Id);
        return participations.Any(p => p.UserId == SessionService.LoggedInUser.Id);
    }


    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        var participation = new Participation
        {
            UserId = SessionService.LoggedInUser?.Id ?? 0,
            ActivityId = _activity.Id,
            Activity = _activity,
            Attend = false
        };

        await _dbService.AddOrUpdateParticipation(participation);
        await _dbService.AddOrUpdateActivity(_activity);

        participation.CreateNotifications();

        await DisplayAlert("Signed Up", "You have successfully signed up for this activity!", "OK");

        LoadActivityDetailsAsync();
    }

    private async void OnSignOutButtonClicked(object sender, EventArgs e)
    {
        var confirmed = await DisplayAlert("Sign Out", "Are you sure you want to sign out from this activity?", "Yes", "No");

        if (confirmed)
        {
            var participation = await _dbService.GetParticipationAsync(SessionService.LoggedInUser.Id, _activity.Id);
            if (participation != null)
            {
                await _dbService.DeleteParticipationAsync(participation);
                await _dbService.AddOrUpdateActivity(_activity);

                await DisplayAlert("Signed Out", "You have successfully signed out of this activity!", "OK");

                LoadActivityDetailsAsync();
            }
        }
    }


}
