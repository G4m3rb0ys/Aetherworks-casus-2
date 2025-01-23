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

        private MainActivityService _activityService;
        private LocalDbService _localDbService;

        private List<VictuzActivity> _allActivities;

        public ActivitiesPage()
        {
            InitializeComponent();

            BindingContext = this;

            _localDbService = new LocalDbService();
            _activityService = new MainActivityService(_localDbService);

            Activities = new ObservableCollection<VictuzActivity>();
            _allActivities = new List<VictuzActivity>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadActivities();
        }

        private void LoadActivities()
        {
            var dbActivities = _activityService.GetAllActivities();
            _allActivities = dbActivities.ToList();

            Activities.Clear();
            foreach (var act in _allActivities)
                Activities.Add(act);

            ActivitiesCollectionView.ItemsSource = Activities;
        }

        private async void OnCreateActivityTapped(object sender, EventArgs e)
        {
            // TODO: verwijzing maken naar nieuwe create pagina nadat deze is gemaakt
            await DisplayAlert("Nieuwe activiteit", "Hier zou je naar CreateActivityPage navigeren DIT WORD ZSM TOEGEVOEGD");
        }

        private void OnFilterTapped(object sender, EventArgs e)
        {
            // TODO: Filter Functionaliteit tovoegen
            DisplayAlert("Filter", "Hier kun je straks activiteiten filteren (bv. betaalstatus, categorie). DIT MOET NOG TOEGEVOEGD WORDEN");
        }

        private void ActivitySearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue?.ToLower() ?? string.Empty;

            var filtered = _allActivities
                .Where(a => a.Name != null && a.Name.ToLower().Contains(keyword))
                .ToList();

            Activities.Clear();
            foreach (var act in filtered)
                Activities.Add(act);
        }
    }
}
