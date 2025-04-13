using Microsoft.Maui.Controls;

using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;

namespace PasswordGenerator.Views
{
    public partial class Vault : ContentPage
    {
        private readonly VaultViewModel _viewModel;

        public Vault(VaultViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadAccountsAsync();
        }

        private async void OnAccountTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAccount());
        }

        private async void OnAddPasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAccount());
        }

        private void OnDarkModeClicked(object sender, EventArgs e)
        {
            var current = Application.Current.UserAppTheme;
            Application.Current.UserAppTheme = current == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
        }

    }
}


