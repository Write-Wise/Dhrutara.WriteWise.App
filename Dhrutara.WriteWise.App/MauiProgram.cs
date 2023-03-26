using Dhrutara.WriteWise.App.LocalStorage;
using Dhrutara.WriteWise.App.Services.Content;

namespace Dhrutara.WriteWise.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddSingleton<AuthService>();

            _ = builder.Services.AddHttpClient<ContentService>(client =>
            {
                Uri? contenApiUri = Uri.TryCreate(Services.Content.Constants.ContentServiceBaseUrl, UriKind.Absolute, out Uri? result) ? result : null;
                client.BaseAddress = contenApiUri;
            });

            builder.Services.AddSingleton<LocalContentProvider>();

            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}