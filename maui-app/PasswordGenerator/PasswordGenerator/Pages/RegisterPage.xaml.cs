using System.Net.Http.Json;
using System.Text.Json;

using PasswordGenerator.Models;

namespace PasswordGenerator.Pages
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (MasterPasswordEntry.Text != ConfirmMasterPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords do not match", "Try Again");
            }
            else
            {
                try
                {
                    string url = "http://10.0.2.2:5000/api/users/v1/register";
                    HttpClient httpClient = new HttpClient();

                    string email = EmailEntry.Text;
                    string password = MasterPasswordEntry.Text;

                    var requestBody = new
                    {
                        email = email,
                        password = password,
                    };

                    var response = await httpClient.PostAsJsonAsync(url, requestBody);
                    var responseJson = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(responseJson);
                    var root = doc.RootElement;

                    if (response.IsSuccessStatusCode)
                    {
                        string userEmail = root.GetProperty("user").GetProperty("email").GetString();
                        string userToken = root.GetProperty("access_token").GetString();
                        User user = new User()
                        {
                            Email = userEmail,
                            Token = userToken
                        };
                        await Navigation.PushAsync(new VaultPage(user));
                    }
                    else if (root.GetProperty("error").GetProperty("code").GetString() == "USER_ALREADY_EXISTS")
                    {
                        await DisplayAlert("Error", "An account with that email already exists.", "OK");
                    }
                    else
                    {
                        string error = root.GetProperty("error").GetProperty("message").GetString();
                        await DisplayAlert("Error", $"An error occurred when registering: {error}", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private async void GoToLoginPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Goes back to LoginPage
        }
    }
}


