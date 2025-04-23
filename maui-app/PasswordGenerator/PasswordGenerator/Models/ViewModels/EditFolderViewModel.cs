using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class EditFolderViewModel : ObservableObject
    {
        private readonly FolderService _folderService;
        private readonly AppState _appState;

        private Folder _originalFolder;

        [ObservableProperty]
        private string folderId;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool isLoading;

        public EditFolderViewModel(FolderService folderService, AppState appState)
        {
            _folderService = folderService;
            _appState = appState;
        }

        public void SetFolder(Folder folder)
        {
            _originalFolder = folder;

            FolderId = folder.Id;
            Name = folder.Name;
        }

        public async Task<OperationResult> UpdateFolderAsync()
        {
            try
            {
                IsLoading = true;

                if (string.IsNullOrEmpty(Name))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Folder name cannot be empty."
                    };
                }

                User user = _appState.GetUser();

                await _folderService.UpdateFolder(user, FolderId, Name);
                return new OperationResult
                {
                    Success = true,
                    Message = "Folder updated successfully."
                };
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
