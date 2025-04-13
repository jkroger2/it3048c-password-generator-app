using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;

using PasswordGenerator.Models.ViewModels;
using PasswordGenerator.Views;
using PasswordGenerator.Services;

namespace PasswordGenerator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            /* DEPENDENCY INJECTION */

            // Register Services
            builder.Services.AddHttpClient<UserService>();
            builder.Services.AddHttpClient<AccountService>();
            builder.Services.AddHttpClient<FolderService>();
            builder.Services.AddHttpClient<PasswordGeneratorService>();

            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<AccountService>();
            builder.Services.AddSingleton<FolderService>();
            builder.Services.AddSingleton<PasswordGeneratorService>();
            builder.Services.AddSingleton<AppState>();

            builder.Services.AddSingleton<AppShell>();

            // Register ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<VaultViewModel>();
            builder.Services.AddTransient<AddAccountViewModel>();
            builder.Services.AddTransient<PasswordGeneratorViewModel>();

            // Register Pages
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<Register>();
            builder.Services.AddTransient<Vault>();
            builder.Services.AddTransient<AddAccount>();
            builder.Services.AddTransient<Views.PasswordGenerator>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}