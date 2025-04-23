using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using PasswordGenerator.Services;
using PasswordGenerator.Models;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class FolderViewModel : ObservableObject
    {
        private readonly FolderService _folderService;
        private readonly AppState _appState;

        public Folder _folder;

        [ObservableProperty]
        private ObservableCollection<Account> accounts = new();

        [ObservableProperty]
        private ObservableCollection<Folder> folders = new();

        [ObservableProperty]
        private string folderId;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool isLoading;

        public FolderViewModel(FolderService folderService, AppState appState)
        {
            _folderService = folderService;
            _appState = appState;
        }

        public void SetFolder(Folder folder)
        {
            _folder = folder;

            folderId = folder.Id;
            Name = folder.Name;
        }

        public void SetAccounts(ObservableCollection<Account> accounts)
        {
            Accounts = accounts;
        }

        public async Task<OperationResult> DeleteFolderAsync()
        {
            try
            {
                IsLoading = true;

                if (_folder == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Folder not found."
                    };
                }

                User user = _appState.GetUser();

                await _folderService.DeleteFolder(user, _folder.Id);

                return new OperationResult
                {
                    Success = true,
                    Message = "Folder deleted successfully."
                };
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
