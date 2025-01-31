using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public partial class QRscanViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string type;

        private readonly int _currentActivityId;

        public QRscanViewModel(LocalDbService dbService, string id, string type, int currentActivityId)
        {
            Id = id;
            Type = type;
            _dbService = dbService;
            _currentActivityId = currentActivityId;
        }

        [RelayCommand]
        public async Task ProcessScannedQRCode(string scannedId)
        {
            if (!SessionService.IsUserLoggedIn)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You need to be logged in.", "OK");
                return;
            }

            var user = SessionService.LoggedInUser;
            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            if (int.TryParse(scannedId, out int parsedId))
            {
                // Case 1: Admin scanning a user QR code at an activity
                if (user.IsAdmin)
                {
                    await MarkUserAsAttended(parsedId, _currentActivityId);
                }
                // Case 2: A user scanning an activity QR code to mark their own attendance
                else
                {
                    await MarkSelfAsAttended(user.Id, parsedId);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Invalid QR Code", "The scanned QR code is invalid.", "OK");
            }
        }

        private async Task MarkUserAsAttended(int userId, int activityId)
        {
            var participation = await _dbService.GetParticipations(activityId);

            var userParticipation = participation.FirstOrDefault(p => p.UserId == userId);

            if (userParticipation != null)
            {
                userParticipation.Attend = true;
                await _dbService.AddOrUpdateParticipation(userParticipation);
                await Application.Current.MainPage.DisplayAlert("Attendance Marked", $"User {userId} marked as attended for Activity {activityId}.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User is not registered for this activity.", "OK");
            }
        }

        private async Task MarkSelfAsAttended(int userId, int activityId)
        {
            var participation = await _dbService.GetParticipations(activityId);

            var selfParticipation = participation.FirstOrDefault(p => p.UserId == userId);

            if (selfParticipation != null)
            {
                selfParticipation.Attend = true;
                await _dbService.AddOrUpdateParticipation(selfParticipation);
                await Application.Current.MainPage.DisplayAlert("Attendance Confirmed", "You have been marked as attended.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You are not registered for this activity.", "OK");
            }
        }
    }
}
