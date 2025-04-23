using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;

namespace PasswordGenerator.Views
{
    public partial class AccountView : ContentPage
    {
        private readonly AccountViewModel _viewModel;
        public AccountView(AccountViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (_viewModel._account != null)
            {
                EditAccountView editAccountView = ((App)Application.Current).Services.GetService<EditAccountView>();
                if (editAccountView != null && editAccountView.BindingContext is EditAccountViewModel editAccountVm)
                {
                    editAccountVm.Folders = new ObservableCollection<Folder>(_viewModel.Folders);
                    editAccountVm.SetAccount(_viewModel._account);
                    await Navigation.PushAsync(editAccountView);
                }
            }
        }

        private async void OnTrashClicked(object sender, EventArgs e)
        {
            if (_viewModel._account != null)
            {
                var result = await _viewModel.DeleteAccountAsync();
                if (!result.Success)
                {
                    await DisplayAlert("Error", result.Message, "OK");
                    return;
                }
                await DisplayAlert("Deleted", "Account has been deleted.", "OK");
                VaultView vaultPage = ((App)Application.Current).Services.GetService<VaultView>();
                await Navigation.PushAsync(vaultPage);

            }
        }
    }
}

