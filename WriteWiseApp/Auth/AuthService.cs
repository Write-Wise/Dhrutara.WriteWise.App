using Microsoft.Identity.Client;

namespace WriteWiseApp.Auth
{
    public class AuthService
    {
        private readonly IPublicClientApplication authenticationClient;
        public AuthService()
        {
            authenticationClient = PublicClientApplicationBuilder
                .Create(Constants.ClientId)
                .WithB2CAuthority(Constants.AuthoritySignIn)
#if ANDROID
                .WithRedirectUri($"msal{Constants.ClientId}://auth")
#elif WINDOWS
.WithRedirectUri("http://localhost/")
#endif
                .Build();
        }

        public async Task<AuthenticationResult?> SigninAsync(CancellationToken cancellationToken)
        {
            AuthenticationResult? result = null;

            IEnumerable<IAccount> accounts = await authenticationClient.GetAccountsAsync(Constants.SignInPolicy).ConfigureAwait(false);
            bool tryInteractive = false;

            try
            {
                result = await authenticationClient.AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
                    .ExecuteAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (MsalUiRequiredException)
            {
                tryInteractive = true;
            }

            if (tryInteractive)
            {
                try
                {
                    result = await authenticationClient
                        .AcquireTokenInteractive(Constants.Scopes)
#if ANDROID
                    .WithParentActivityOrWindow(Platform.CurrentActivity)
#endif
                        .ExecuteAsync(cancellationToken);

                }
                catch (MsalClientException)
                {
                    return null;
                }
            }

            return result;
        }

        public async Task SignoutAsync(CancellationToken cancellationToken)
        {
            IEnumerable<IAccount> accounts = await authenticationClient.GetAccountsAsync().ConfigureAwait(false);

            if(accounts?.Any() == true)
            {
                foreach(IAccount? account in accounts)
                {
                    await authenticationClient.RemoveAsync(account).ConfigureAwait(false);
                }
            }
        }
    }
}
