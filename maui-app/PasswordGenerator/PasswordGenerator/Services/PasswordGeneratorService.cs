using System.Net.Http.Json;
using System.Text.Json;

namespace PasswordGenerator.Services
{
    public class PasswordGeneratorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://10.0.2.2:5000/api/password/v1/generate"; // Base URL for the generator endpoint

        public PasswordGeneratorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GeneratePassword(int length, bool numbers, bool lowercase, bool uppercase, bool symbols)
        {
            string url = _baseUrl;
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

        public async Task<string> GeneratePassphrase(int wordCount, bool capitalize)
        {
            string url = $"{_baseUrl}/passphrase";
            var requestBody = new
            {
                word_count = wordCount,
                capitalize = capitalize
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                var passphrase = root.GetProperty("passphrase").GetString();
                return passphrase;
            }
            else
            {
                throw new Exception($"Error generating passphrase: {response.StatusCode}");
            }
        }
    }
}
