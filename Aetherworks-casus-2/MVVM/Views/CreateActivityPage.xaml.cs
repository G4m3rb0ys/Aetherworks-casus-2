using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;
using Microsoft.Maui.Storage;

namespace Aetherworks_casus_2.MVVM.Views
{
    public partial class CreateActivityPage : ContentPage
    {
        private readonly LocalDbService _localDbService;
        private ObservableCollection<string> _categories;
        private string _selectedImagePath;

        public CreateActivityPage()
        {
            InitializeComponent();

            _localDbService = new LocalDbService();

            _categories = new ObservableCollection<string>(
                Enum.GetNames(typeof(ActivityCategory)).ToList()
            );
            _categories.Add("Custom");
            CategoryPicker.ItemsSource = _categories;
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnCategoryPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            CustomCategoryEntry.IsVisible = CategoryPicker.SelectedItem?.ToString() == "Custom";
        }

        private async void OnSelectPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an Image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    _selectedImagePath = result.FullPath;
                    ActivityImage.Source = ImageSource.FromFile(_selectedImagePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to pick a photo: {ex.Message}", "OK");
            }
        }

        private async void OnCreateButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(LocationEntry.Text) ||
                ActivityDatePicker.Date == null ||
                ActivityTimePicker.Time == null)
            {
                await DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            string category;
            if (CategoryPicker.SelectedItem?.ToString() == "Custom")
            {
                if (string.IsNullOrWhiteSpace(CustomCategoryEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter a custom category name.", "OK");
                    return;
                }
                category = CustomCategoryEntry.Text;

                if (!_categories.Contains(category))
                {
                    _categories.Insert(_categories.Count - 1, category);
                }
            }
            else
            {
                category = CategoryPicker.SelectedItem?.ToString();
            }

            var activity = new VictuzActivity
            {
                Name = NameEntry.Text,
                Description = DescriptionEditor.Text,
                ActivityDate = ActivityDatePicker.Date.Add(ActivityTimePicker.Time),
                Location = new VictuzLocation { Name = LocationEntry.Text },
                Category = Enum.TryParse(category, out ActivityCategory result) ? result : ActivityCategory.Other,
                Price = decimal.TryParse(PriceEntry.Text, out var price) ? price : 0,
                MemberPrice = decimal.TryParse(MemberPriceEntry.Text, out var memberPrice) ? memberPrice : 0,
                ParticipationLimit = int.TryParse(ParticipationLimitEntry.Text, out var limit) ? limit : 0,
                HostId = 1, // Replace with dynamic host ID if needed
                Picture = _selectedImagePath
            };

            await _localDbService.InsertActivityAsync(activity);

            await DisplayAlert("Success", "Activity created successfully!", "OK");

            await Navigation.PopAsync();
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var confirmed = await DisplayAlert("Cancel", "Are you sure you want to cancel?", "Yes", "No");
            if (confirmed)
            {
                await Navigation.PopAsync();
            }
        }
    }
}
