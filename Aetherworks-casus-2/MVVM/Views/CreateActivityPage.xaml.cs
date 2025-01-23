using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.Views
{
    public partial class CreateActivityPage : ContentPage
    {
        private readonly MainActivityService _activityService;

        // Enum values converted to a list
        private ObservableCollection<string> _categories;

        public CreateActivityPage()
        {
            InitializeComponent();
            _activityService = new MainActivityService();

            // Initialize Categories (add "Custom" option)
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
            // Check if "Custom" is selected
            if (CategoryPicker.SelectedItem?.ToString() == "Custom")
            {
                CustomCategoryEntry.IsVisible = true; // Show the custom category entry
            }
            else
            {
                CustomCategoryEntry.IsVisible = false; // Hide it
            }
        }

        private async void OnCreateButtonClicked(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(LocationEntry.Text) ||
                ActivityDatePicker.Date == null ||
                ActivityTimePicker.Time == null)
            {
                await DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            // Determine the selected category
            string category;
            if (CategoryPicker.SelectedItem?.ToString() == "Custom")
            {
                if (string.IsNullOrWhiteSpace(CustomCategoryEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter a custom category name.", "OK");
                    return;
                }
                category = CustomCategoryEntry.Text;

                // Add the new category to the Picker for future use
                if (!_categories.Contains(category))
                {
                    _categories.Insert(_categories.Count - 1, category); // Add before "Custom"
                }
            }
            else
            {
                category = CategoryPicker.SelectedItem?.ToString();
            }

            // Create a new activity
            var activity = new VictuzActivity
            {
                Name = NameEntry.Text,
                Description = DescriptionEditor.Text,
                ActivityDate = ActivityDatePicker.Date.Add(ActivityTimePicker.Time),
                Location = new VictuzLocation { Name = LocationEntry.Text }, // Assuming Location is a separate entity
                Category = Enum.TryParse(category, out ActivityCategory result) ? result : ActivityCategory.Other,
                Price = decimal.TryParse(PriceEntry.Text, out var price) ? price : 0,
                MemberPrice = decimal.TryParse(MemberPriceEntry.Text, out var memberPrice) ? memberPrice : 0,
                ParticipationLimit = int.TryParse(ParticipationLimitEntry.Text, out var limit) ? limit : 0,
                HostId = 1 // Assuming a hardcoded HostId for now
            };

            // Save to database
            _activityService.InsertActivity(activity);
            await DisplayAlert("Success", "Activity created successfully!", "OK");

            // Navigate back
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
