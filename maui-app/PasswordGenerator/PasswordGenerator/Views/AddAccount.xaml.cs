using Microsoft.Maui.Controls;

namespace PasswordGenerator.Views
{
    public partial class AddAccount : ContentPage
    {
        public AddAccount()
        {
            InitializeComponent();
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


