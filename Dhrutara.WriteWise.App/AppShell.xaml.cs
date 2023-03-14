using System.Windows.Input;

namespace Dhrutara.WriteWise.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void MenuPrivacyPolicy_Clicked(object? sender, EventArgs e)
        {
            await AppShell.OpenUrlInBrowserAsync(new Uri("https://writewise.dhrutara.net/privacy-policy/"));
        }

        private async void MenuTermsOfService_Clicked(object? sender, EventArgs e)
        {
            await AppShell.OpenUrlInBrowserAsync(new Uri("https://writewise.dhrutara.net/terms-of-service/"));
        }

        private async void MenuUserDataDeletion_Clicked(object? sender, EventArgs e)
        {
            await AppShell.OpenUrlInBrowserAsync(new Uri("https://writewise.dhrutara.net/user-data-deletion/"));
        }

        private static async Task OpenUrlInBrowserAsync(Uri url)
        {
            try
            {
                BrowserLaunchOptions options = new()
                {
                    LaunchMode = BrowserLaunchMode.External,
                    TitleMode = BrowserTitleMode.Show
                };

                await Browser.Default.OpenAsync(url, options);
            }
            catch
            {
                // An unexpected error occurred. No browser may be installed on the device.
            }
        }

    }
}