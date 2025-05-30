using Microsoft.Maui.Controls;

using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;

namespace PasswordGenerator.Views
{
    public partial class AddAccountView : ContentPage
    {
        private readonly AddAccountViewModel _viewModel;
        public AddAccountView(AddAccountViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await _viewModel.AddAccountAsync();

                if (!result.Success)
                {
                    await DisplayAlert("Error", result.Message, "OK");
                    return;
                }

                await DisplayAlert("Saved", "Changes have been saved.", "OK");

                VaultView vaultView = ((App)Application.Current).Services.GetService<VaultView>();
                await Navigation.PushAsync(vaultView);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}


