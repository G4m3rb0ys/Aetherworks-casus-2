using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.Views
{
    public partial class ActivitiesPage : ContentPage
    {
        public ObservableCollection<VictuzActivity> Activities { get; set; }

        private readonly LocalDbService _localDbService;
        private List<VictuzActivity> _allActivities;

        public ActivitiesPage()
        {
            InitializeComponent();

            BindingContext = this;

            _localDbService = new LocalDbService();
            Activities = new ObservableCollection<VictuzActivity>();
            _allActivities = new List<VictuzActivity>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadActivitiesAsync();
        }

        private async Task LoadActivitiesAsync()
        {
            var dbActivities = await _localDbService.GetAllActivities();
            _allActivities = dbActivities.ToList();

            Activities.Clear();
            foreach (var act in _allActivities)
            {
                Activities.Add(act);
            }

            ActivitiesCollectionView.ItemsSource = Activities;
        }

        private async void OnCreateActivityTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateActivityPage());
        }

        private void OnFilterTapped(object sender, EventArgs e)
        {
            DisplayAlert("Filter", "Filter functionality will be added here.", "OK");
        }

        private void ActivitySearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue?.ToLower() ?? string.Empty;

            var filtered = _allActivities
                .Where(a => a.Name != null && a.Name.ToLower().Contains(keyword))
                .ToList();

            Activities.Clear();
            foreach (var act in filtered)
            {
                Activities.Add(act);
            }
        }

        private async void OnActivityTapped(object sender, EventArgs e)
        {
            var activity = (sender as Frame)?.BindingContext as VictuzActivity;
            if (activity != null)
            {
                await Navigation.PushAsync(new ActivityPage(activity, _localDbService));
            }
        }
    }
}
