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
        private readonly string[] _words;

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

            // Load word list from embedded resource
            try
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("Resources/word-list.txt").Result;
                using var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                _words = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                
                if (_words.Length == 0)
                {
                    throw new Exception("Word list is empty");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load word list: {ex.Message}");
            }
        }

        [RelayCommand]
        public void TogglePasswordMode()
        {
            IsPasswordMode = !IsPasswordMode;
            IsPassphraseMode = !IsPasswordMode;
            GeneratedPassword = string.Empty;
        }

        private string GeneratePassphrase()
        {
            if (_words == null || _words.Length == 0)
                return "Error: Word list not available";

            var random = new Random();
            var words = new List<string>();

            for (int i = 0; i < WordCount; i++)
            {
                string word = _words[random.Next(_words.Length)].ToLower();
                if (Uppercase)
                    word = char.ToUpper(word[0]) + word.Substring(1);
                words.Add(word);
            }

            return string.Join(" ", words);
        }

        [RelayCommand]
        public async Task GeneratePasswordAsync()
        {
            try
            {
                IsLoading = true;
                if (IsPasswordMode)
                {
                    string generated = await _passwordGeneratorService.GeneratePassword(Length, Numbers, Lowercase, Uppercase, Symbols);
                    GeneratedPassword = generated;
                }
                else
                {
                    GeneratedPassword = GeneratePassphrase();
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
