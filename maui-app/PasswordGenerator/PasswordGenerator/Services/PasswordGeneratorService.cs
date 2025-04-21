using System.Net.Http.Json;
using System.Text.Json;

namespace PasswordGenerator.Services
{
    public class PasswordGeneratorService
    {
        private readonly HttpClient _httpClient;

        public PasswordGeneratorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GeneratePassword(int length, bool numbers, bool lowercase, bool uppercase, bool symbols)
        {
            string url = "http://localhost:5000/api/password/v1/generate";
            var requestBody = new
            {
                length = length,
                numbers = numbers,
                lowercase = lowercase,
                uppercase = uppercase,
                symbols = symbols
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                var password = root.GetProperty("password").GetString();

                return password;
            }
            else
            {
                throw new Exception($"Error generating password: {response.StatusCode}");
            }
        }
    }
}
