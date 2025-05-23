﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Models;
using PasswordGenerator.Services;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class EditAccountViewModel : ObservableObject
    {
        private readonly AccountService _accountService;
        private readonly AppState _appState;

        private Account _originalAccount;

        [ObservableProperty]
        private string accountId;

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
        private Folder selectedFolder;

        [ObservableProperty]
        private ObservableCollection<Folder> folders = new();

        [ObservableProperty]
        private bool hidePassword = true;

        [ObservableProperty]
        private bool isLoading;

        public EditAccountViewModel(AccountService accountService, AppState appState)
        {
            _accountService = accountService;
            _appState = appState;
        }

        public void SetAccount(Account account)
        {
            _originalAccount = account;

            AccountId = account.Id;
            Name = account.Name;
            Username = account.Username;
            Password = account.Password;
            Url = account.Url;
            FolderId = account.FolderId;

            selectedFolder = Folders.FirstOrDefault(f => f.Id == account.FolderId);
        }

        public void SetFolders(ObservableCollection<Folder> folders)
        {
            Folders = folders;
        }

        public async Task<OperationResult> UpdateAccountAsync()
        {
            try
            {

                isLoading = true;

                if (_originalAccount == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Account not loaded."
                    };
                }

                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Name, username, and password cannot be empty."
                    };
                }

                User user = _appState.GetUser();

                Account updatedAccount = await _accountService.UpdateAccount(user, AccountId, Name, Username, Password, Url, selectedFolder.Id);

                SetAccount(updatedAccount);

                return new OperationResult
                {
                    Success = true,
                    Message = "Account updated successfully.",
                };
            }
            finally
            {
                isLoading = false;
            }
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
    }
}
