using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Aetherworks_casus_2.MVVM.Views;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public class ProfileViewModel
    {
        public User? LoggedInUser { get; set; } = SessionService.LoggedInUser;
        public string? Username { get; set; } = SessionService.LoggedInUser.Username;
        public string? Email { get; set; } = SessionService.LoggedInUser.Email;
        public string? Name { get; set; } = SessionService.LoggedInUser.Name;
        public string? PhoneNumber { get; set; } = SessionService.LoggedInUser.PhoneNumber;

        public ICommand GenerateQRCodeCommand { get; }

        public ProfileViewModel()
        {
            GenerateQRCodeCommand = new RelayCommand(OnGenerateQRCode);
        }

        private async void OnGenerateQRCode()
        {
            if (LoggedInUser != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new QRCodeGeneratorPage(LoggedInUser.Id.ToString(), "User"));
            }
        }
        public void LogOut()
        {
            SessionService.LogOut();
        }
    }
}
