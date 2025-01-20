using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly LocalDbService _dbService;
        private readonly MainActivityService _activityservice;

        private VictuzActivity? _nextActivity;
        public VictuzActivity? NextActivity
        {
            get => _nextActivity;
            set { _nextActivity = value; OnPropertyChanged(); }
        }

        private ObservableCollection<VictuzActivity> _upcomingActivities;
        public ObservableCollection<VictuzActivity> UpcomingActivities
        {
            get => _upcomingActivities;
            set { _upcomingActivities = value; OnPropertyChanged(); }
        }

        private List<DateTime> _specialDates;
        public List<DateTime> SpecialDates
        {
            get => _specialDates;
            set { _specialDates = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Parameterloze constructor (nodig als je de ViewModel rechtstreeks in XAML wilt instantiëren).
        /// </summary>
        public MainPageViewModel()
        {
            _dbService = new LocalDbService();
            _activityservice = new MainActivityService();
            LoadActivities();
        }

        public MainPageViewModel(LocalDbService dbService, MainActivityService activityService)
        {
            _dbService = dbService;
            _activityservice = activityService;
            LoadActivities();
        }

        private void LoadActivities()
        {
            var allActivities = _activityservice.GetAllActivities();

            var sorted = allActivities.OrderBy(a => a.ActivityDate).ToList();

            NextActivity = sorted.FirstOrDefault(a => a.ActivityDate >= DateTime.Now);

            if (NextActivity != null)
            {
                UpcomingActivities = new ObservableCollection<VictuzActivity>(
                    sorted
                      .Where(a => a.ActivityDate > NextActivity.ActivityDate)
                      .Take(3));
            }
            else
            {
                UpcomingActivities = new ObservableCollection<VictuzActivity>();
            }

            _specialDates = allActivities
                .Select(a => a.ActivityDate.Date)
                .Distinct()
                .ToList();
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
