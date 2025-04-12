using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

using PasswordGenerator.Models;

namespace PasswordGenerator.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(EmailEntry.Text) && !string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            try
            {
                string url = "http://10.0.2.2:5000/api/users/v1/login";
                HttpClient httpClient = new HttpClient();

                string email = EmailEntry.Text;
                string password = PasswordEntry.Text;

                var requestBody = new
                {
                    email = email,
                    password = password,
                };

                var response = await httpClient.PostAsJsonAsync(url, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(responseJson);
                    var root = doc.RootElement;

                    string userEmail = root.GetProperty("user").GetProperty("email").GetString();
                    string userAccessToken = root.GetProperty("access_token").GetString();

                    User user = new User()
                    {
                        Email = userEmail,
                        AccessToken = userAccessToken
                    };

                    await Navigation.PushAsync(new VaultPage(user));
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await DisplayAlert("Error", "Invalid email or password.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "An unexpected error occurred", "OK");
                }
            } 
            catch (Exception ex)
            {
                await DisplayAlert("Network Error", ex.Message, "Ok");
            }
           
        }
        else
        {
            await DisplayAlert("Error", "Email or Password cannot be empty.", "OK");
        }
    }

    private async void GoToRegisterPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
