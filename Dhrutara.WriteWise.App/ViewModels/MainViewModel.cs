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

        [RelayCommand]
        private async void OnNewContentClicked()
        {
            NewContentOptionsView popup = new()
            {
                CanBeDismissedByTappingOutsideOfPopup = false,
                Color = new Color(255, 255, 255)
            };

            object? result = await Page.ShowPopupAsync(popup);

            if (result is ContentOptions newContentOptionsResult && newContentOptionsResult != null)
            {
                if (newContentOptionsResult != null)
                {
                    NewContentOptions = newContentOptionsResult;
                }

                await ShowNewContent();
            }
        }

        private async Task SignInAsync(CancellationToken cancellationToken)
        {
            UserContext? userContext = await _authService.SigninAsync(true, cancellationToken);
            WelcomeMessage = $"Hi {userContext?.GivenName ?? "there"}, welcome to Write Wise!";
        }

        //private async Task SignOutAsync()
        //{
        //    await _authService.SignoutAsync();

        //    Message = "Sign in";
        //    SemanticScreenReader.Announce("Signed out");
        //}

        public async Task ShowNewContent()
        {
            await Task.Yield();
            Message = $"A {NewContentOptions.Category} {NewContentOptions.Type} to {NewContentOptions.Receiver} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        }
    }
}