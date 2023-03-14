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

            builder.Services.AddOptions();

            //using Stream? configStream = Assembly
            //    .GetExecutingAssembly()
            //    .GetManifestResourceStream("Dhrutara.WriteWise.App.appsettings.json");

            //if (configStream != null)
            //{
            //    var config = new ConfigurationBuilder()
            //    .AddJsonStream(configStream)
            //    .Build();

            //    _ = builder.Configuration.AddConfiguration(config);
            //}


            builder.Services.AddSingleton<AuthService>();

            _ = builder.Services.AddHttpClient<ContentService>(client =>
            {
                //string? configuredUri = builder.Configuration.GetSection("Settings").GetSection("ContentApiUri").Value;
                Uri? contenApiUri = Uri.TryCreate(Services.Content.Constants.ContentServiceBaseUrl, UriKind.Absolute, out Uri? result) ? result : null;
                client.BaseAddress = contenApiUri;
            });

            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<MainPage>();

            // TODO: Add App Center secrets
            AppCenter.Start(
                "windowsdesktop={Your Windows App secret here};" +
                "android={Your Android App secret here};" +
                "ios={Your iOS App secret here};" +
                "macos={Your macOS App secret here};",
                typeof(Analytics), typeof(Crashes));

            return builder.Build();
        }
    }
}