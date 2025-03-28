using System;
using Microsoft.Maui.Controls;

namespace PasswordGenerator
{
    public partial class EditItemPage : ContentPage
    {
        public EditItemPage()
        {
            InitializeComponent();
        }

        private void OnSocialMediaTapped(object sender, EventArgs e)
        {
            SocialMediaAccounts.IsVisible = !SocialMediaAccounts.IsVisible;
        }
        private void OnFinancialTapped(object sender, EventArgs e)
        {
            FinancialAccounts.IsVisible = !FinancialAccounts.IsVisible;
        }

        private void OnWorkTapped(object sender, EventArgs e)
        {
            WorkAccounts.IsVisible = !WorkAccounts.IsVisible;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Placeholder action
            await DisplayAlert("Saved", "Changes have been saved.", "OK");

            // Optional: Navigate back
            await Shell.Current.GoToAsync("..");
        }
    }
}
