using System;
using Microsoft.Maui.Controls;

using PasswordGenerator.Models;
using PasswordGenerator.Models.ViewModels;

namespace PasswordGenerator.Views
{
    public partial class PasswordGenerator : ContentPage
    {
        private readonly PasswordGeneratorViewModel _viewModel;


        public PasswordGenerator(PasswordGeneratorViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private void OnGenerateClicked(object sender, EventArgs e)
        {
            _viewModel.GeneratePasswordAsync();
        }

        private void OnDarkModeClicked(object sender, EventArgs e)
        {
            var current = Application.Current.UserAppTheme;
            Application.Current.UserAppTheme = current == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
        }
    }


}

