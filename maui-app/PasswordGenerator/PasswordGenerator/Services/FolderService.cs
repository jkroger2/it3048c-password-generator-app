using System.Net.Http.Json;
using System.Text.Json;

using PasswordGenerator.Models;


namespace PasswordGenerator.Services
{
    public class FolderService
    {
        private readonly HttpClient _httpClient;
        public FolderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Folder>> GetUserFolders(User user)
        {
            List<Folder> folders = new List<Folder>();

            string url = "http://10.0.2.2:5000/api/folders/v1/";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);

            var response = await _httpClient.GetAsync(url);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                var folderssList = root.GetProperty("folders");

                foreach (var fol in folderssList.EnumerateArray())
                {
                    var folderId = fol.GetProperty("id").GetString();
                    var folderName = fol.GetProperty("name").GetString();

                    Folder folder = new Folder()
                    {
                        Id = folderId,
                        Name = folderName
                    };

                    folders.Add(folder);
                }

                return folders;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return folders;
            }
            else
            {
                throw new Exception("Error fetching folders");
            }
        }

        public async Task<Folder> CreateFolder(User user, string name)
        {
            string url = "http://10.0.2.2:5000/api/folders/v1/";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);

            var requestBody = new
            {
                name = name
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                var fol = root.GetProperty("folder");
                var folderId = fol.GetProperty("id").GetString();
                var folderName = fol.GetProperty("name").GetString();

                Folder folder = new Folder()
                {
                    Id = folderId,
                    Name = folderName
                };

                return folder;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }
            else
            {
                throw new Exception("Error creating folder");
            }
        }

        public async Task<Folder> UpdateFolder(User user, string id, string name)
        {
            string url = $"http://10.0.2.2:5000/api/folders/v1/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);

            var requestBody = new
            {
                name = name
            };

            var response = await _httpClient.PutAsJsonAsync(url, requestBody);
            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                var fol = root.GetProperty("folder");
                var folderId = fol.GetProperty("id").GetString();
                var folderName = fol.GetProperty("name").GetString();

                Folder folder = new Folder()
                {
                    Id = folderId,
                    Name = folderName
                };

                return folder;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }
            else
            {
                throw new Exception("Error updating folder");
            }
        }

        public async Task DeleteFolder(User user, string id)
        {
            string url = $"http://10.0.2.2:5000/api/folders/v1/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }
            else
            {
                throw new Exception("Error deleting folder");
            }
        }
    }
}
