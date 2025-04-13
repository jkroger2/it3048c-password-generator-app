using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;
using PasswordGenerator.Views;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly AppState _appState;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string masterPassword;

        [ObservableProperty]
        private string confirmMasterPassword;

        [ObservableProperty]
        private bool isBusy;

        public RegisterViewModel(UserService userService, AppState appState)
        {
            _userService = userService;
            _appState = appState;
        }

        [RelayCommand]
        public async Task<OperationResult> RegisterAsync()
        {
            try
            {
                IsBusy = true;

                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(MasterPassword) || string.IsNullOrEmpty(ConfirmMasterPassword))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Email and password cannot be empty."
                    };
                }
                if (MasterPassword != ConfirmMasterPassword)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Passwords do not match."
                    };
                }

                User user = await _userService.RegisterAndLogin(Email, MasterPassword);

                _appState.SetUser(user);
                return new OperationResult
                {
                    Success = true,
                    Message = "Registration successful."
                };
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
