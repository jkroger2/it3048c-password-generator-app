using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;


namespace PasswordGenerator.Views
{
    public partial class VaultView : ContentPage
    {
        private readonly VaultViewModel _viewModel;

        public VaultView(VaultViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadAccountsAsync();
            await _viewModel.LoadFoldersAsync();
        }

        private async void OnAccountTapped(object sender, TappedEventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is Account account)
            {
                AccountView accountView = ((App)Application.Current).Services.GetService<AccountView>();
                AccountViewModel accountVm = (AccountViewModel)accountView.BindingContext;

                accountVm.Folders = new ObservableCollection<Folder>(_viewModel.Folders);
                accountVm.SetAccount(account);

                await Navigation.PushAsync(accountView);
            }
        }

        private async void OnFolderTapped(object sender, TappedEventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is Folder folder)
            {
                var folderAccounts = new ObservableCollection<Account>(_viewModel.Accounts.Where(a => a.FolderId == folder.Id));

                FolderView folderView = ((App)Application.Current).Services.GetService<FolderView>();
                FolderViewModel folderVm = (FolderViewModel)folderView.BindingContext;

                folderVm.SetFolder(folder);
                folderVm.SetAccounts(folderAccounts);

                await Navigation.PushAsync(folderView);
            }
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            string chosenAction = await DisplayActionSheet("Add New", "Cancel", null, "Password", "Folder");

            if (chosenAction == "Password")
            {
                AddAccountView addAccountView = ((App)Application.Current).Services.GetService<AddAccountView>();
                AddAccountViewModel addAccountVm = (AddAccountViewModel)addAccountView.BindingContext;

                addAccountVm.Folders = new ObservableCollection<Folder>(_viewModel.Folders);

                await Navigation.PushAsync(addAccountView);
            }
            else if (chosenAction == "Folder")
            {
                AddFolderView addFolderView = ((App)Application.Current).Services.GetService<AddFolderView>();
                await Navigation.PushAsync(addFolderView);
            }
        }

        private void OnDarkModeClicked(object sender, EventArgs e)
        {
            var current = Application.Current.UserAppTheme;
            Application.Current.UserAppTheme = current == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
        }
    }
}


