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

        public ObservableCollection<VictuzActivity> Activities { get; set; }
        public VictuzActivity FirstActivity { get; set; }
        public bool HasUpcomingActivity => FirstActivity != null;

        public MainPage()
        {
            InitializeComponent();
            _dbService = new LocalDbService();
            Activities = new ObservableCollection<VictuzActivity>();
            BindingContext = new MainViewModel(new LocalDbService());

            LoadActivities();
        }

        private async void LoadActivities()
        {
            var activities = await _dbService.GetAllActivities();
            if (activities.Any())
            {
                FirstActivity = activities.OrderBy(a => a.ActivityDate).FirstOrDefault();
                Activities.Clear();
                foreach (var activity in activities)
                {
                    Activities.Add(activity);
                }
            }
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
    }
}
