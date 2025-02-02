using System;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views
{
    public partial class MainPage : ContentPage
    {
        private bool _isMenuOpen = false;
        private readonly LocalDbService _dbService;
        private readonly bool _isAdmin;

        public ObservableCollection<VictuzActivity> Activities { get; set; }
        public VictuzActivity FirstActivity { get; set; }
        public bool HasUpcomingActivity { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _dbService = new LocalDbService();
            FirstActivity = Activities.OrderBy(a => a.ActivityDate).FirstOrDefault();
            Activities = new ObservableCollection<VictuzActivity>();
            BindingContext = this;
            ToggleShake();
            _isAdmin = SessionService.LoggedInUser?.IsAdmin ?? false;

            if (_isAdmin)
            {
                ScanEventButton.IsVisible = false;
            }

            LoadActivities();
        }

        private async void LoadActivities()
        {
            var activities = await _dbService.GetAllActivities();
            if (activities.Any())
            {
                HasUpcomingActivity = FirstActivity != null;
                int x = 0;
                Activities.Clear();
                foreach (var activity in activities)
                {
                    if (x != 3 && activity.ActivityDate >= DateTime.Now)
                    {
                        Activities.Add(activity);
                        x++;
                    }
                }
            }
        }

        private async void OnScanTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QRscanPage());
        }

        private async void OnHamburgerTapped(object sender, EventArgs e)
        {
            _isMenuOpen = !_isMenuOpen;
            await MainContainer.TranslateTo(_isMenuOpen ? 220 : 0, 0, 250, Easing.Linear);
        }

        private void OnProfileTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void OnBellTapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new NotificationsPage());
        }

        private async void OnActivityTapped(object sender, EventArgs e)
        {
            var activity = (sender as Frame)?.BindingContext as VictuzActivity;
            if (activity != null)
            {
                await Navigation.PushAsync(new ActivityPage(activity, _dbService));
            }
        }
        private void ToggleShake()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    // Turn on accelerometer
                    Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                    Accelerometer.Default.Start(SensorSpeed.Game);
                }
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            MainContainer.BackgroundColor = new Color(Random.Shared.Next(256), Random.Shared.Next(256), Random.Shared.Next(256));
            BackgroundColor = new Color(Random.Shared.Next(256), Random.Shared.Next(256), Random.Shared.Next(256));
        }
    }
}
