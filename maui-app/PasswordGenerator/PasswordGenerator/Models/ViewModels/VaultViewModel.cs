using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class VaultViewModel : ObservableObject
    {
        private readonly AccountService _accountService;
        private readonly FolderService _folderService;
        private readonly AppState _appState;

        [ObservableProperty]
        public ObservableCollection<Account> accounts = new();

        [ObservableProperty]
        public ObservableCollection<Folder> folders = new();

        [ObservableProperty]
        private bool isLoading;

        public VaultViewModel(AccountService accountService, FolderService folderService, AppState appState)
        {
            _accountService = accountService;
            _folderService = folderService;
            _appState = appState;
        }

        [RelayCommand]
        public async Task LoadAccountsAsync()
        {
            try
            {
                IsLoading = true;

                User user = _appState.GetUser();

                var accountsList = await _accountService.GetUserAccounts(user);
                Accounts = new ObservableCollection<Account>(accountsList ?? []);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task LoadFoldersAsync()
        {
            try
            {
                IsLoading = true;

                User user = _appState.GetUser();

                var foldersList = await _folderService.GetUserFolders(user);
                Folders = new ObservableCollection<Folder>(foldersList ?? []);
   
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
