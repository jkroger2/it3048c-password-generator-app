using PasswordGenerator.Services;
using PasswordGenerator.Models;

namespace PasswordGenerator.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly UserService _userService;
        public LoginPage(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Email or Password field cannot be empty.", "OK");
            }
            else
            {
                try
                {
                    string email = EmailEntry.Text;
                    string password = PasswordEntry.Text;

                    User user = await _userService.AuthenticateUser(email, password);
                    await Navigation.PushAsync(new VaultPage(user));
                }
                catch (UnauthorizedAccessException)
                {
                    await DisplayAlert("Error", "Invalid email or password.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private async void GoToRegisterPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }

}

