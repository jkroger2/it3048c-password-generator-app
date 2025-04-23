using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class AccountViewModel : ObservableObject
    {
        private readonly AccountService _accountService;
        private readonly AppState _appState;

        public Account _account;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string url;

        [ObservableProperty]
        private string folderId;

        [ObservableProperty]
        private ObservableCollection<Folder> folders = new();

        [ObservableProperty]
        private Folder selectedFolder;

        [ObservableProperty]
        private bool hidePassword = true;

        [ObservableProperty]
        private bool isLoading;

        public AccountViewModel(AccountService accountService, AppState appState)
        {
            _accountService = accountService;
            _appState = appState;
        }

        public void SetAccount(Account account)
        {
            _account = account;

            Name = account.Name;
            Username = account.Username;
            Password = account.Password;
            Url = account.Url;
            FolderId = account.FolderId;

            selectedFolder = Folders.FirstOrDefault(f => f.Id == account.FolderId);
        }

        [RelayCommand]
        public async Task CopyPasswordAsync()
        {
            if (!string.IsNullOrEmpty(Password))
            {
                await Clipboard.SetTextAsync(Password);
            }
        }

        [RelayCommand]
        public void ToggleVisibility()
        {
            HidePassword = !HidePassword;
        }

        public async Task<OperationResult> DeleteAccountAsync()
        {
            try
            {
                isLoading = true;

                if (_account == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Account not loaded."
                    };
                }

                User user = _appState.GetUser();

                await _accountService.DeleteAccount(user, _account.Id);

                return new OperationResult
                {
                    Success = true,
                    Message = "Account deleted successfully."
                };
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
