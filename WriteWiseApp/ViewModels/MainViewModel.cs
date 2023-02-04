using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WriteWiseApp.Auth;


namespace WriteWiseApp.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly AuthService _authService;
    public MainViewModel(AuthService authService)
    {
        _authService = authService;
    }

    [ObservableProperty]
    public string message = "Sign in";

    [RelayCommand]
    private async void SignInOutClicked()
    {
        if (Message.Equals("SIGN IN", StringComparison.OrdinalIgnoreCase))
        {
            await SignInAsync(CancellationToken.None);
        }
        else
        {
            await SignOutAsync(CancellationToken.None);
        }

    }

    private async Task SignInAsync(CancellationToken cancellationToken)
    {
        AuthenticationResult? result = await _authService.SigninAsync(cancellationToken);

        string token = result?.IdToken ?? string.Empty;

        string message = string.Empty;

        if (token != null)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken data = handler.ReadJwtToken(token);
            List<System.Security.Claims.Claim> claims = data.Claims.ToList();
            if (data != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"Name: {data.Claims.FirstOrDefault(x => x.Type.Equals("name"))?.Value}");
                stringBuilder.AppendLine($"Email: {data.Claims.FirstOrDefault(x => x.Type.Equals("preferred_username"))?.Value}");
                message = stringBuilder.ToString();
            }
        }

        Message = "Sign out";

        SemanticScreenReader.Announce("Signed in");
    }

    private async Task SignOutAsync(CancellationToken cancellationToken)
    {
        await _authService.SignoutAsync(cancellationToken);

        Message = "Sign in";
        SemanticScreenReader.Announce("Signed out");
    }
}
