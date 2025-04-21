using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;

using PasswordGenerator.Services;

namespace PasswordGenerator.Models.ViewModels
{
    public partial class PasswordGeneratorViewModel : ObservableObject
    {
        private readonly PasswordGeneratorService _passwordGeneratorService;
        private readonly AppState _appState;

        [ObservableProperty]
        private bool isPasswordMode = true;

        [ObservableProperty]
        private bool isPassphraseMode = false;

        [ObservableProperty]
        private string generatedPassword;

        [ObservableProperty]
        private int length;

        [ObservableProperty]
        private int wordCount = 4;

        [ObservableProperty]
        private bool numbers;

        [ObservableProperty]
        private bool lowercase;

        [ObservableProperty]
        private bool uppercase;

        [ObservableProperty]
        private bool symbols;

        [ObservableProperty]
        private bool isLoading;

        public PasswordGeneratorViewModel(PasswordGeneratorService passwordGeneratorService, AppState appState)
        {
            _passwordGeneratorService = passwordGeneratorService;
            _appState = appState;
            Length = 12; // Default length
            Numbers = true; // Default to include numbers
            Lowercase = true; // Default to include lowercase letters
            Uppercase = true; // Default to include uppercase letters
            Symbols = true; // Default to include symbols
        }

        [RelayCommand]
        public void TogglePasswordMode()
        {
            IsPasswordMode = !IsPasswordMode;
            IsPassphraseMode = !IsPasswordMode;
            GeneratedPassword = string.Empty;
        }

        [RelayCommand]
        public async Task GeneratePasswordAsync()
        {
            try
            {
                IsLoading = true;
                if (IsPasswordMode)
                {
                    GeneratedPassword = await _passwordGeneratorService.GeneratePassword(Length, Numbers, Lowercase, Uppercase, Symbols);
                }
                else
                {
                    GeneratedPassword = await _passwordGeneratorService.GeneratePassphrase(WordCount, Uppercase);
                }
            }
            catch (Exception ex)
            {
                GeneratedPassword = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task CopyPasswordAsync()
        {
            if (!string.IsNullOrEmpty(GeneratedPassword))
            {
                await Clipboard.SetTextAsync(GeneratedPassword);
            }
        }
    }
}
