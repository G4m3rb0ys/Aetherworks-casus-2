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
using AndroidX.Camera.Core;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using ImageAnalysis = Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.ImageAnalysis;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public class ProfileViewModel
    {
        public User? LoggedInUser { get; set; } = SessionService.LoggedInUser;
        public string? Username { get; set; } = SessionService.LoggedInUser.Username;
        public string? Email { get; set; } = SessionService.LoggedInUser.Email;
        public string? Name { get; set; } = SessionService.LoggedInUser.Name;
        public string? PhoneNumber { get; set; } = SessionService.LoggedInUser.PhoneNumber;
        public Byte[]? ProfilePicture { get; set; } = SessionService.LoggedInUser.ProfilePicture;


        private LocalDbService _db = new LocalDbService();

        private readonly string _apiKey = "1HBk9fdF8A6IsHY99cMQ8YcMP2CtXVHCDQnaqCNuSAvMMZpmJMe5JQQJ99BAAC5RqLJXJ3w3AAAFACOGNpXD";
        private readonly string _endpoint = "https://datingappnsfwdetectionapi.cognitiveservices.azure.com/";

        public ICommand GenerateQRCodeCommand { get; }

        public ProfileViewModel()
        {
            GenerateQRCodeCommand = new RelayCommand(OnGenerateQRCode);
            
        }

        public ImageSource ShowProfilePicture()
        {
            if (SessionService.LoggedInUser?.ProfilePicture != null)
            {
                return ImageSource.FromStream(() => new MemoryStream(SessionService.LoggedInUser?.ProfilePicture));
            }
            return null;
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

        public async Task<bool> SaveProfilePicture(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            SessionService.LoggedInUser.ProfilePicture = imageBytes;

            if (await _db.AddOrUpdateUser(SessionService.LoggedInUser) != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> CheckImageForNSFW(string imageUrl)
        {
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_apiKey))
            {
                Endpoint = _endpoint
            };

            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
        {
            VisualFeatureTypes.Adult
        };
            using var imageStream = File.OpenRead(imageUrl);
            ImageAnalysis results = await client.AnalyzeImageInStreamAsync(imageStream, features);

            return !(results.Adult.IsAdultContent || results.Adult.IsRacyContent || results.Adult.IsGoryContent);

        }
    }
}
