namespace Dhrutara.WriteWise.App
{
    public partial class AppShell : Shell
    {
        private const string AUTOMATION_ID_MENU_PRIVACY_POLICY = "menuPrivacyPolicy";
        private const string AUTOMATION_ID_MENU_TERMS_OF_SERVICE = "menuTermsOfService";
        private const string AUTOMATION_ID_MENU_USER_DATA_DELETION = "menuUserDataDeletion";
        private const string AUTOMATION_ID_MENU_SIGN_OUT = "menuSignout";
        private const string AUTOMATION_ID_MENU_ACCOUNT_DELETION = "menuAccountDeletion";
        private const string AUTOMATION_ID_MENU_SIGN_IN_OR_UP = "menuSigninOrUp";

        private readonly AuthService _authService;
        public AppShell(AuthService authService)
        {
            _authService = authService;
            InitializeComponent();
            BuildMenuItemsAsync().GetAwaiter().GetResult();
        }


        private async void MenuPrivacyPolicy_Clicked(object? sender, EventArgs e)
        {
            await AppShell.OpenUrlInBrowserAsync(new Uri("https://writewise.dhrutara.net/privacy-policy/"));
        }


        private async void Testing_clicked(object? sender, EventArgs e)
        {
            await AppShell.OpenUrlInBrowserAsync(new Uri("https://writewise.dhrutara.net/terms-of-service/"));
        }

        private async void MenuTermsOfService_Clicked(object? sender, EventArgs e)
        {
            await AppShell.OpenUrlInBrowserAsync(new Uri("https://writewise.dhrutara.net/terms-of-service/"));
        }

        private async void MenuUserDataDeletion_Clicked(object? sender, EventArgs e)
        {
            await AppShell.OpenUrlInBrowserAsync(new Uri("https://writewise.dhrutara.net/user-data-deletion/"));
        }

        private async void MenuSignout_Clicked(object? sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Sign out confirmation", "Are you sure you want to sign out Write Wise?", "Yes", "No");
            if (answer)
            {
                Shell.Current.FlyoutIsPresented = false;
                await _authService.SignoutAsync();
                await BuildMenuItemsAsync();
            }
        }

        private async void MenuSignInorUp_Clicked(object? sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await _authService.SigninAsync(true, CancellationToken.None);
            await BuildMenuItemsAsync();
        }

        private async void MenuAccountDeletion_Clicked(object? sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Account deletion confirmation", "Are you sure you want to delete your Write Wise accuont?", "Yes", "No");
            if (answer)
            {
                Shell.Current.FlyoutIsPresented = false;
                await _authService.DeleteUserAccountAsync();
                await BuildMenuItemsAsync();
            }
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

        private async Task BuildMenuItemsAsync()
        {

             
            if(!shell.Items.Any(i => AUTOMATION_ID_MENU_PRIVACY_POLICY.Equals(i.AutomationId))) {
                MenuItem menuPrivacyPolicy = new() { Text = "Privacy Policy", AutomationId = AUTOMATION_ID_MENU_PRIVACY_POLICY };
                menuPrivacyPolicy.Clicked += MenuPrivacyPolicy_Clicked;
                shell.Items.Add(menuPrivacyPolicy);
            }

            if (!shell.Items.Any(i => AUTOMATION_ID_MENU_TERMS_OF_SERVICE.Equals(i.AutomationId)))
            {
                MenuItem menuTermsOfService = new() { Text = "Terms of Service", AutomationId= AUTOMATION_ID_MENU_TERMS_OF_SERVICE };
                menuTermsOfService.Clicked += MenuTermsOfService_Clicked;
                shell.Items.Add(menuTermsOfService);
            }

                

            if (!shell.Items.Any(i => AUTOMATION_ID_MENU_USER_DATA_DELETION.Equals(i.AutomationId)))
            {
                MenuItem menuUserDataDeletion = new() { Text = "User Data Deletion Process", AutomationId= AUTOMATION_ID_MENU_USER_DATA_DELETION };
                menuUserDataDeletion.Clicked += MenuUserDataDeletion_Clicked;
                shell.Items.Add(menuUserDataDeletion);
            }

            UserContext? user = await _authService.SigninAsync(false, CancellationToken.None);
            if(user != null)
            {
                if (!shell.Items.Any(i => AUTOMATION_ID_MENU_SIGN_OUT.Equals(i.AutomationId)))
                {
                    MenuItem menuSignout = new() { Text = "Sign out", AutomationId= AUTOMATION_ID_MENU_SIGN_OUT };
                    menuSignout.Clicked += MenuSignout_Clicked;
                    shell.Items.Add(menuSignout);
                }

                if (!shell.Items.Any(i => AUTOMATION_ID_MENU_ACCOUNT_DELETION.Equals(i.AutomationId)))
                {
                    MenuItem menuAccountDeletion = new() { Text = "Delete my Account",AutomationId= AUTOMATION_ID_MENU_ACCOUNT_DELETION };
                    menuAccountDeletion.Clicked += MenuAccountDeletion_Clicked;
                    shell.Items.Add(menuAccountDeletion);
                }

                var signItem = shell.Items.FirstOrDefault(i => AUTOMATION_ID_MENU_SIGN_IN_OR_UP.Equals(i.AutomationId));
                if(signItem != null)
                {
                    int index = shell.Items.IndexOf(signItem);
                    shell.Items.RemoveAt(index);
                }
            }
            else
            {
                if (!shell.Items.Any(i => AUTOMATION_ID_MENU_SIGN_IN_OR_UP.Equals(i.AutomationId)))
                {
                    MenuItem menuSigninOrUp = new() { Text = "Sign in/Sign up", AutomationId=AUTOMATION_ID_MENU_SIGN_IN_OR_UP };
                    menuSigninOrUp.Clicked += MenuSignInorUp_Clicked;
                    shell.Items.Add(menuSigninOrUp);
                }


                var signOutItem = shell.Items.FirstOrDefault(i => AUTOMATION_ID_MENU_SIGN_OUT.Equals(i.AutomationId));
                if (signOutItem != null)
                {
                    int index = shell.Items.IndexOf(signOutItem);
                    shell.Items.RemoveAt(index);
                }

                var deleteAccountItem = shell.Items.FirstOrDefault(i => AUTOMATION_ID_MENU_ACCOUNT_DELETION.Equals(i.AutomationId));
                if (deleteAccountItem != null)
                {
                    int index = shell.Items.IndexOf(deleteAccountItem);
                    shell.Items.RemoveAt(index);
                }

            }
        }
    }
}