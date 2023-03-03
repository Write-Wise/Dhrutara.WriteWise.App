using Dhrutara.WriteWise.App.Services.Content;
using CommunityToolkit.Maui.Views;

namespace Dhrutara.WriteWise.App.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;
        private readonly ContentService _contentService;
        private readonly AuthService _authService;

        public MainPage(MainViewModel viewModel, ContentService contentService, AuthService authService)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
            _contentService = contentService;
            _authService = authService;

            TrySignInAsync(CancellationToken.None).SafeFireAndForget();
        }

        private async void OnContentDoubleTapped(object sender, TappedEventArgs e)
        {
            await Task.Yield();
            await Share.Default.RequestAsync(new ShareTextRequest { 
                Text = _viewModel.Message,
                Title = $"Share{_viewModel.NewContentOptions.Type.ToString()}"
            });
        }

        private async void OnContentTapped(object sender, TappedEventArgs e)
        {
            await ShowNewContentAsync(_viewModel.NewContentOptions);
        }

        private async void OnNewContentClicked(object sender, EventArgs e)
        {
            NewContentOptionsView popup = new(_viewModel.NewContentOptions)
            {
                CanBeDismissedByTappingOutsideOfPopup = false,
                Color = new Color(255, 255, 255)
            };

            object? result = await this.ShowPopupAsync(popup);

            if (result is ContentOptions newContentOptionsResult && newContentOptionsResult != null)
            {
                if (newContentOptionsResult != null)
                {
                    _viewModel.NewContentOptions = newContentOptionsResult;
                }

                await ShowNewContentAsync(_viewModel.NewContentOptions);
            }
        }

        private async Task ShowNewContentAsync(ContentOptions options)
        {
            string[] contentLines = await GetNewContentAsync(options, CancellationToken.None);
            string? newMessage = contentLines.Any()
                ? contentLines.Aggregate((l, r) => $"{l}{Environment.NewLine}{r}")
                : null;

            _viewModel.Message = newMessage ?? "Something went wrong, please try again!";
        }

        private Task<string[]> GetNewContentAsync(ContentOptions options, CancellationToken cancellationToken)
        {
            ApiRequest request = new()
            {
                Category = options.Category,
                From = options.Receiver,
                Type = options.Type
            };

            return _contentService.GetContentAsync(request, cancellationToken);
        }

        private async Task TrySignInAsync(CancellationToken cancellationToken)
        {
            UserContext? userContext = await _authService.SigninAsync(true, cancellationToken);
            _viewModel.WelcomeMessage = $"Hi {userContext?.GivenName ?? "there"}, welcome to Write Wise!";
        }
    }
}