using PasswordGenerator.Models;

namespace PasswordGenerator.Services
{
    public class AppState
    {
        private User CurrentUser { get; set; }

        public User GetUser() => CurrentUser;

        public void SetUser(User user)
        {
            CurrentUser = user;
        }

        public string GetUserToken() => CurrentUser?.Token;
    }
}
