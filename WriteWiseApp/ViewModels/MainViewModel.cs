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
            await SignOutAsync();
        }

    }

    private async Task SignInAsync(CancellationToken cancellationToken)
    {
        UserContext? userContext = await _authService.SigninAsync(true, cancellationToken);
        
        if(userContext != null)
        {
            Message = "Sign out";
            SemanticScreenReader.Announce("Signed in");
        }
    }

    private async Task SignOutAsync()
    {
        await _authService.SignoutAsync();

        Message = "Sign in";
        SemanticScreenReader.Announce("Signed out");
    }
}
