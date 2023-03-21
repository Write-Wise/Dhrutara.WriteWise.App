using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;

namespace Dhrutara.WriteWise.App.Services.Auth;

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
        if (User == null || !User.IsAccessTokenValid())
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

        if (accounts?.Any() == true)
        {
            foreach (IAccount? account in accounts)
            {
                await authenticationClient.RemoveAsync(account).ConfigureAwait(false);
            }
        }

        ClearUser();
    }

    internal async Task DeleteUserAccountAsync()
    {
        await Task.Yield();
    }

    private static void SetUser(AuthenticationResult? authResult)
    {
        if (authResult != null)
        {
            User = new()
            {
                AccountIdentifer = authResult.UniqueId ?? authResult.Account.HomeAccountId.Identifier,
                AccessToken = authResult.AccessToken,
                UserName = authResult.Account.Username,
                ExpiresOn = authResult.ExpiresOn,

            };

            string token = authResult.IdToken ?? string.Empty;

            if (token != null)
            {
                JwtSecurityTokenHandler handler = new();
                JwtSecurityToken data = handler.ReadJwtToken(token);
                List<System.Security.Claims.Claim>? claims = data?.Claims.ToList();
                if (data?.Claims?.Any() == true)
                {
                    string userEmails = data.Claims.FirstOrDefault(c => c.Type.Equals("emails"))?.Value ?? string.Empty;
                    User.Email = userEmails.Split(',').FirstOrDefault();
                    User.FamilyName = data.Claims.FirstOrDefault(c => c.Type.Equals("family_name"))?.Value;
                    User.GivenName = data.Claims.FirstOrDefault(c => c.Type.Equals("given_name"))?.Value;
                    User.UserName = data.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
                }
            }
        }
    }

    private static void ClearUser()
    {
        User = null;
    }
}