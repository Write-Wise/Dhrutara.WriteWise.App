using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;

namespace WriteWiseApp.Auth
{
    public class AuthService
    {
        private static UserContext? User { get; set; }
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

        internal async Task<UserContext?> SigninAsync(bool tryInteractiveLogin, CancellationToken cancellationToken)
        {
            if (User == null)
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
                    tryInteractive = tryInteractiveLogin;
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

                SetUser(result);
            }

            return User;
        }

        internal async Task SignoutAsync()
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

        private static void SetUser(AuthenticationResult? authResult)
        {
            if (authResult != null)
            {
                User = new()
                {
                    AccesToken= authResult.AccessToken,
                    UserName = authResult.Account.Username
                };

                string token = authResult.IdToken ?? string.Empty;

                if (token != null)
                {
                    JwtSecurityTokenHandler handler = new();
                    JwtSecurityToken data = handler.ReadJwtToken(token);
                    var claims = data?.Claims.ToList();
                    if (data?.Claims?.Any() == true)
                    {
                        User.GivenName = data.Claims.FirstOrDefault(c => c.Type.Equals("given_name"))?.Value;
                        User.FamilyName = data.Claims.FirstOrDefault(c => c.Type.Equals("family_name"))?.Value;
                        string userEmails = data.Claims.FirstOrDefault(c => c.Value.Equals("emails"))?.Value??string.Empty;
                        User.Email = userEmails.Split(',').FirstOrDefault();
                    }
                }
            }
        }
    }
}
