using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class AddFolderViewModel : ObservableObject
    {
        private readonly FolderService _folderService;
        private readonly AppState _appState;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool isLoading;

        public AddFolderViewModel(FolderService folderService, AppState appState)
        {
            _folderService = folderService;
            _appState = appState;
        }

        public async Task<OperationResult> AddFolderAsync()
        {
            try
            {
                isLoading = true;

                if (string.IsNullOrEmpty(Name))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Folder name cannot be empty."
                    };
                }

                User user = _appState.GetUser();

                await _folderService.CreateFolder(user, Name);

                return new OperationResult
                {
                    Success = true,
                    Message = "Folder created successfully."
                };
            }
            finally
            {
                isLoading = false;
            }
        }
    }

}
