namespace PasswordGenerator.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Dummy check – add validation if needed
        if (MasterPasswordEntry.Text == ConfirmMasterPasswordEntry.Text)
        {
            await DisplayAlert("Success", "Account created!", "OK");
            await Navigation.PushAsync(new LoginPage());
        }
        else
        {
            await DisplayAlert("Error", "Passwords do not match", "Try Again");
        }
    }

    private async void GoToLoginPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Goes back to LoginPage
    }
}
