using System.Net.Http.Json;
using System.Text.Json;

using PasswordGenerator.Models;

namespace PasswordGenerator.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<Account>> GetUserAccounts(User user)
        {
            string url = "http://10.0.2.2:5000/api/accounts/v1";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);

            var response = await _httpClient.GetAsync(url);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                var accountsList = root.GetProperty("accounts");
                List<Account> accounts = new List<Account>();

                foreach (var acc in accountsList.EnumerateArray())
                {
                    var accountId = acc.GetProperty("id").GetString();
                    var accountName = acc.GetProperty("name").GetString();
                    var accountUsername = acc.GetProperty("username").GetString();
                    var accountPassword = acc.GetProperty("password").GetString();
                    var accountUrl = acc.GetProperty("url").GetString();
                    var accountFavicon = acc.GetProperty("favicon").GetString();
                    var accountFolderId = acc.GetProperty("folder_id").GetString();

                    Account account = new Account()
                    {
                        Id = accountId,
                        Name = accountName,
                        Username = accountUsername,
                        Password = accountPassword,
                        Url = accountUrl,
                        Favicon = accountFavicon,
                        FolderId = accountFolderId
                    };
                    accounts.Add(account);
                }

                return accounts;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }
            else
            {
                throw new Exception("An error occurred when fetching accounts.");
            }
        }

        public async Task<Account> CreateAccount(User user, string name, string username, string password, string url, string folderId)
        {
            string requestUrl = "http://10.0.2.2:5000/api/accounts/v1";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);

            var requestBody = new
            {
                name = name,
                username = username,
                password = password,
                url = url,
                folder_id = folderId
            };

            var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                var acc = root.GetProperty("account");
                var accountId = acc.GetProperty("id").GetString();
                var accountName = acc.GetProperty("name").GetString();
                var accountUsername = acc.GetProperty("username").GetString();
                var accountPassword = acc.GetProperty("password").GetString();
                var accountUrl = acc.GetProperty("url").GetString();
                var accountFavicon = acc.GetProperty("favicon").GetString();
                var accountFolderId = acc.GetProperty("folder_id").GetString();

                Account account = new Account()
                {
                    Id = accountId,
                    Name = accountName,
                    Username = accountUsername,
                    Password = accountPassword,
                    Url = accountUrl,
                    Favicon = accountFavicon,
                    FolderId = accountFolderId
                };

                return account;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }
            else
            {
                throw new Exception("Error occurred when creating account.");
            }
        }
    }
}