using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.Data;
using static ZXing.RGBLuminanceSource;
using ZXing.Net.Maui;
using Microsoft.Maui.Graphics;
using CommunityToolkit.Mvvm.Input;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.MVVM.Views;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public partial class ActivityViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private bool isAdmin;

        [ObservableProperty]
        private VictuzActivity activity;

        public ActivityViewModel(LocalDbService dbService, VictuzActivity activity)
        {
            _dbService = dbService;
            Activity = activity;
            IsAdmin = SessionService.LoggedInUser?.IsAdmin ?? false;
        }

        [RelayCommand]
        private async void GenerateQRCode()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new QRCodeGeneratorPage(Activity.Id.ToString(), "Activity"));
        }
    }
}
