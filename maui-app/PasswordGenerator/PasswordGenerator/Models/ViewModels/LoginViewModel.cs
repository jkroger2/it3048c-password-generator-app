using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;
using PasswordGenerator.Views;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly AppState _appState;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isBusy;

        public LoginViewModel(UserService userService, AppState appState)
        {
            _userService = userService;
            _appState = appState;
        }

        [RelayCommand]
        public async Task<OperationResult> LoginAsync()
        {
            try
            {
                IsBusy = true;
                
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Email and password cannot be empty."
                    };
                }

                User user = await _userService.AuthenticateUser(Email, Password);

                _appState.SetUser(user);
                return new OperationResult
                {
                    Success = true,
                    Message = "Login successful."
                };
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
