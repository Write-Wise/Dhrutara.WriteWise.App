using CommunityToolkit.Maui.Views;

namespace Dhrutara.WriteWise.App.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        static Page Page => Application.Current?.MainPage ?? throw new NullReferenceException();

        private readonly AuthService _authService;
        public MainViewModel(AuthService authService)
        {
            _authService = authService;

            SignInAsync(CancellationToken.None).GetAwaiter().GetResult();

            newContentOptions ??= new ContentOptions
            {
                Category = ContentCategory.None,
                Type = ContentType.Joke,
                Receiver = Relationship.None
            };
        }

        [ObservableProperty]
        public string welcomeMessage = string.Empty;

        [ObservableProperty]
        public string message = string.Empty;

        [ObservableProperty]
        public ContentOptions newContentOptions;

        private async Task SignInAsync(CancellationToken cancellationToken)
        {
            UserContext? userContext = await _authService.SigninAsync(true, cancellationToken);
            WelcomeMessage = $"Hi {userContext?.GivenName ?? "there"}, welcome to Write Wise!";
        }
    }
}