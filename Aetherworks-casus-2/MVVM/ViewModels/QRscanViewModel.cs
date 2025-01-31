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

        private int _currentActivityId;

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
            var user = SessionService.LoggedInUser;
            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            if (int.TryParse(scannedId, out int parsedId))
            {
                // Case 1: Admin scanning a user QR code at an activity
                if (user.IsAdmin && _currentActivityId != 0)
                {
                    await MarkUserAsAttended(parsedId, _currentActivityId);
                }
                // Case 2: A user scanning an activity QR code from ActivityPage
                else if (!user.IsAdmin && _currentActivityId == 0)
                {
                    await MarkSelfAsAttended(user.Id, parsedId);
                }
                // Case 3: A user scanning an activity QR code from MainPage
                else if (!user.IsAdmin && _currentActivityId == 0)
                {
                    _currentActivityId = parsedId; // Use scanned activity ID
                    await MarkSelfAsAttended(user.Id, _currentActivityId);
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
                _dbService.AddOrUpdateParticipation(userParticipation);
                await Application.Current.MainPage.DisplayAlert("Attendance Marked", $"User {userId} marked as attended for Activity {activityId}.", "OK");

                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User is not registered for this activity.", "OK");

                Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async Task MarkSelfAsAttended(int userId, int activityId)
        {
            var participation = await _dbService.GetParticipations(activityId);
            var selfParticipation = participation.FirstOrDefault(p => p.UserId == userId);

            if (selfParticipation != null)
            {
                selfParticipation.Attend = true;
                _dbService.AddOrUpdateParticipation(selfParticipation);
                Application.Current.MainPage.DisplayAlert("Attendance Confirmed", "You have been marked as attended.", "OK");

                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "You are not registered for this activity.", "OK");

                Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
