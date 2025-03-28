using Microsoft.Maui.Controls;

namespace PasswordGenerator
{
    public partial class VaultPage : ContentPage
    {
        public VaultPage()
        {
            InitializeComponent();
        }

        private async void OnAccountTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PasswordGenerator.EditItemPage));
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

        private void OnEntertainmentTapped(object sender, EventArgs e)
        {
            EntertainmentAccounts.IsVisible = !EntertainmentAccounts.IsVisible;
        }

        private async void OnAddPasswordClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddPasswordPage));
        }
    }
}
