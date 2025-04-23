using Microsoft.Maui.Controls;

using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;

namespace PasswordGenerator.Views
{
    public partial class EditAccountView : ContentPage
    {
        private readonly EditAccountViewModel _viewModel;
        public EditAccountView(EditAccountViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await _viewModel.UpdateAccountAsync();

                if (!result.Success)
                {
                    await DisplayAlert("Error", result.Message, "OK");
                    return;
                }

                await DisplayAlert("Saved", "Changes have been saved.", "OK");

                VaultView vaultPage = ((App)Application.Current).Services.GetService<VaultView>();
                await Navigation.PushAsync(vaultPage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

}

