using System;
using Microsoft.Maui.Controls;

namespace Aetherworks_casus_2.MVVM.Views
{
    public partial class MainPage : ContentPage
    {
        private bool _isMenuOpen = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnHamburgerTapped(object sender, EventArgs e)
        {
            _isMenuOpen = !_isMenuOpen;

            if (_isMenuOpen)
            {
                await MainContainer.TranslateTo(220, 0, 250, Easing.Linear);
            }
            else
            {
                await MainContainer.TranslateTo(0, 0, 250, Easing.Linear);
            }
        }

        private void OnProfileTapped(object sender, EventArgs e)
        {
            DisplayAlert("Profiel", "Mijn prachtige profiel (moet nog toegevoegd worden)", "OK");
        }

        private void OnBellTapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new NotificationsPage());
        }
    }
}
