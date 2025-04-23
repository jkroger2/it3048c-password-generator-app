using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;


namespace PasswordGenerator.Views;

public partial class FolderView : ContentPage
{
    private readonly FolderViewModel _viewModel;
    public FolderView(FolderViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
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

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (_viewModel._folder != null)
        {
            EditFolderView editFolderView = ((App)Application.Current).Services.GetService<EditFolderView>();
            if (editFolderView != null && editFolderView.BindingContext is EditFolderViewModel editFolderVm)
            {
                editFolderVm.SetFolder(_viewModel._folder);
                await Navigation.PushAsync(editFolderView);
            }
        }
    }

    private async void OnTrashClicked(object sender, EventArgs e)
    {
        if (_viewModel._folder != null)
        {
            var result = await _viewModel.DeleteFolderAsync();

            if (!result.Success)
            {
                await DisplayAlert("Error", result.Message, "OK");
                return;
            }

            await DisplayAlert("Deleted", "Folder has been deleted.", "OK");
            
            VaultView vaultPage = ((App)Application.Current).Services.GetService<VaultView>();
            await Navigation.PushAsync(vaultPage);
        }
    }
}