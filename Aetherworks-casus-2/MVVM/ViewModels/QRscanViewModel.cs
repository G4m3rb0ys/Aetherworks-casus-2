using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.MVVM.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    internal partial class QRscanViewModel
    {
        private readonly LocalDbService _dbService;

        public QRscanViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task AddUserToParticipation(int activityId)
        {
            if (!SessionService.IsUserLoggedIn)
            {
                return;
            }

            var user = SessionService.LoggedInUser;

            if (user == null)
            {
                return;
            }

            var participation = new Participation
            {
                UserId = user.Id,
                ActivityId = activityId,
                Attend = true
            };

            try
            {
                await _dbService.AddParticipationAsync(participation);
            }
            catch
            {

            }
        }

        [RelayCommand]
        public void BackButton()
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
