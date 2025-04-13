namespace PasswordGenerator.Services
{
    public class FolderService
    {
        private readonly HttpClient _httpClient;
        public FolderService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
    }
}
