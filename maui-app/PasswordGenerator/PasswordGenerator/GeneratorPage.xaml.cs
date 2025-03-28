using System;
using Microsoft.Maui.Controls;

namespace PasswordGenerator
{
    public partial class GeneratorPage : ContentPage
    {
        private bool isPasswordMode = true;

        public GeneratorPage()
        {
            InitializeComponent();
            UpdateButtonStyles();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (LengthLabel != null)
                LengthLabel.Text = ((int)e.NewValue).ToString();
        }

        private void OnPasswordClicked(object sender, EventArgs e)
        {
            isPasswordMode = true;
            GeneratedOutput.Text = "GeneratedPassword123!";
            UpdateButtonStyles();
        }

        private void OnPassphraseClicked(object sender, EventArgs e)
        {
            isPasswordMode = false;
            GeneratedOutput.Text = "purple-ocean-cactus";
            UpdateButtonStyles();
        }

        private void UpdateButtonStyles()
        {
            if (PasswordButton != null && PassphraseButton != null)
            {
                PasswordButton.BackgroundColor = isPasswordMode ? Colors.LightGray : Colors.White;
                PassphraseButton.BackgroundColor = !isPasswordMode ? Colors.LightGray : Colors.White;
            }
        }

        private void OnDarkModeClicked(object sender, EventArgs e)
        {
            var current = Application.Current.UserAppTheme;
            Application.Current.UserAppTheme = current == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
        }

        private void OnGenerateClicked(object sender, EventArgs e)
        {
            string generated = "";
            GeneratedOutput.Text = generated;
        }

    }
}

