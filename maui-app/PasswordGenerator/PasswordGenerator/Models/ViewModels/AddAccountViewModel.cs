﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class AddAccountViewModel : ObservableObject
    {
        private readonly AccountService _accountService;
        private readonly AppState _appState;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string url;

        [ObservableProperty]
        private string favicon;

        [ObservableProperty]
        private string folderId;

        [ObservableProperty]
        private bool isLoading;

        public AddAccountViewModel(AccountService accountService, AppState appState)
        {
            _accountService = accountService;
            _appState = appState;
        }

        [RelayCommand]
        public async Task<OperationResult> AddAccountAsync()
        {
            try
            {
                IsLoading = true;

                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Name, username, and password cannot be empty."
                    };
                }

                User user = _appState.GetUser();

                await _accountService.CreateAccount(user, Name, Username, Password, Url, FolderId);
                return new OperationResult
                {
                    Success = true,
                    Message = "Account created successfully."
                };
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
