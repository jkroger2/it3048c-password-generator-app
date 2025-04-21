using System.Net.Http.Json;
using System.Text.Json;

using PasswordGenerator.Models;

namespace PasswordGenerator.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            string url = "http://10.0.2.2:5000/api/users/v1/login";
            var requestBody = new
            {
                email = email,
                password = password,
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
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

                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            else
            {
                throw new Exception("An error occurred when logging in.");
            }
        }

        public async Task RegisterUser(string email, string password)
        {
            string url = "http://10.0.2.2:5000/api/users/v1/register";
            var requestBody = new
            {
                email = email,
                password = password,
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else if (root.GetProperty("error").GetProperty("code").GetString() == "USER_ALREADY_EXISTS")
            {
                throw new Exception("A user with this email already exists.");
            }
            else
            {
                throw new Exception("An error occurred when registering.");
            }
        }

        public async Task<User> RegisterAndLogin(string email, string password)
        {
            await RegisterUser(email, password);
            User authenticatedUser = await AuthenticateUser(email, password);
            
            return authenticatedUser;
        }
    }
}
