using CommunityToolkit.Maui.Views;

namespace Dhrutara.WriteWise.App.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    static Page Page => Application.Current?.MainPage ?? throw new NullReferenceException();

    private readonly AuthService _authService;
    public MainViewModel(AuthService authService)
    {
        _authService = authService;
        newContentOptions = new NewContentOptions
        {
            Category = ContentCategory.None,
            Type = ContentType.Joke,
            Receiver = Relationship.None
        };
    }

    [ObservableProperty]
    public string message = "Sign in";

    [ObservableProperty]
    public NewContentOptions newContentOptions;

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

    [RelayCommand]
    private async void OnNewContentClicked()
    {
        var popup = new NewContentOptionsView()
        {
            CanBeDismissedByTappingOutsideOfPopup = false,
            Color = new Color(255,255,255)
        };

        var result = await Page.ShowPopupAsync(popup);

        if(result is NewContentOptions newContentOptionsResult && newContentOptionsResult != null)
        {
            if(newContentOptionsResult != null) {
                NewContentOptions = newContentOptionsResult;
            }
            
            Message = $"Type: {NewContentOptions.Type}, Category: {NewContentOptions.Category} and Receiver relationship: {NewContentOptions.Receiver}";
        }
    }

    private async Task SignInAsync(CancellationToken cancellationToken)
    {
        UserContext? userContext = await _authService.SigninAsync(true, cancellationToken);

        if (userContext != null)
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
