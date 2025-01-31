using Aetherworks_casus_2.MVVM.Views.Authentication;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.ViewModels;
using SQLite;
using Android.Gms.Common.Api.Internal;
using AndroidX.Camera.Core;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using ImageAnalysis = Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.ImageAnalysis;

namespace Aetherworks_casus_2.MVVM.Views
{
    public partial class ProfilePage : ContentPage
    {
        private LocalDbService _db = new LocalDbService();
        private ProfileViewModel ProfileView = new ProfileViewModel();

        public ProfilePage()
        {
            InitializeComponent();

            ProfilePicture.Source = ProfileView.ShowProfilePicture();

            BindingContext = ProfileView;
        }

        private async void OnGenerateQRCodeClicked(object sender, EventArgs e)
        {
            if (ProfileView.LoggedInUser != null)
            {
                await Navigation.PushAsync(new QRCodeGeneratorPage(ProfileView.LoggedInUser.Id.ToString(), "User"));
            }
        }

        private void OnLogOutClicked(object sender, EventArgs e)
        {
            ProfileView.LogOut();
            App.Current.MainPage = new NavigationPage(new StartPage(_db));
        }

        private string _selectedImagePath;

        private async void OnUploadClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an Image"
                });

                if (result != null)
                {
                    _selectedImagePath = result.FullPath;

                    if (!await ProfileView.CheckImageForNSFW(_selectedImagePath))
                    {
                        await DisplayAlert("Warning", "The image contains explicit content", "OK");
                        _selectedImagePath = "";
                        SaveButton.IsVisible = false;
                        return;
                    }

                    ProfilePicture.Source = ImageSource.FromFile(_selectedImagePath);

                    SaveButton.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        public async void OnSaveClicked(object sender, EventArgs e)
        {
            if (await ProfileView.SaveProfilePicture(_selectedImagePath))
            {
                await DisplayAlert("Success", "Your profile picture will be updated shortly", "OK");
            }
        }

    }
}