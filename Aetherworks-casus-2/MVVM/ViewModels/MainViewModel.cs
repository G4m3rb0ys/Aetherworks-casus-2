using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.MVVM.Views;
using Microsoft.Maui.Controls;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private VictuzActivity firstActivity;

        [ObservableProperty]
        private ObservableCollection<VictuzActivity> activities = new();

        public bool HasUpcomingActivity => FirstActivity != null;

        public MainViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            LoadActivities();
        }

        private async void LoadActivities()
        {
            var allActivities = await _dbService.GetAllActivities();
            if (allActivities != null && allActivities.Any())
            {
                var sortedActivities = allActivities.OrderBy(a => a.ActivityDate).ToList();

                FirstActivity = sortedActivities.FirstOrDefault();
                Activities.Clear();
                foreach (var activity in sortedActivities.Skip(1).Take(3)) // Alleen de 3 volgende activiteiten tonen
                {
                    Activities.Add(activity);
                }

                OnPropertyChanged(nameof(HasUpcomingActivity));
            }
        }

        [RelayCommand]
        private async Task OpenActivity(VictuzActivity activity)
        {
            if (activity == null) return;
            await Application.Current.MainPage.Navigation.PushAsync(new ActivityPage(activity, _dbService));
        }
    }
}
