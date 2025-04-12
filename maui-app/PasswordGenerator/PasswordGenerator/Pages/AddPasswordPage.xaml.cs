using Microsoft.Maui.Controls;

namespace PasswordGenerator.Pages;

public partial class AddPasswordPage : ContentPage
{
    public AddPasswordPage()
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
