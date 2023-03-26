using Dhrutara.WriteWise.App.LocalStorage;
using Dhrutara.WriteWise.App.Services.Content;

namespace Dhrutara.WriteWise.App.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;
        private readonly ContentService _contentService;
        private readonly AuthService _authService;
        private readonly LocalContentProvider _localContentProvider;

        public MainPage(MainViewModel viewModel, ContentService contentService, AuthService authService, LocalContentProvider localContentProvider)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
            _contentService = contentService;
            _authService = authService;
            _localContentProvider = localContentProvider;

            TrySignInAsync(CancellationToken.None).SafeFireAndForget();
        }

        private async void OnContentDoubleTapped(object sender, TappedEventArgs e)
        {
            await Share.Default.RequestAsync(new ShareTextRequest { 
                Text = _viewModel.Message,
                Title = $"Share{_viewModel.NewContentOptions.Type}"
            });
        }

        private async void OnContentTapped(object sender, TappedEventArgs e)
        {
            if (_viewModel.EnableContentRefresh)
            {
                await ShowNewContentAsync(_viewModel.NewContentOptions);
            }
            else
            {
                await ShowContentChoicesPopupAsync(CancellationToken.None);
            }
            
        }

        private async void OnSelectContentChoicesClicked(object sender, EventArgs e)
        {
            await ShowContentChoicesPopupAsync(CancellationToken.None);
        }

        private async Task ShowContentChoicesPopupAsync(CancellationToken cancellationToken)
        {
            SelectContentChoicesViewModel input = new(_viewModel.NewContentOptions);

            UserContext? user = await _authService.SigninAsync(false, cancellationToken);

            input.ContentTypes = user != null 
                ? GetEnumToTypes<ContentType>(true, false)
                : GetEnumToTypes<ContentType>(true, false, (x) => _localContentProvider.SupportedContentTypes.Contains(x));
            input.ContentCategories = user != null
                ? GetEnumToTypes<ContentCategory>(true, false)
                : GetEnumToTypes<ContentCategory>(true, false, (x) => _localContentProvider.SupportedContentCategories.Contains(x));
            input.ReceiverRelationships = user != null
                ? GetEnumToTypes<Relationship>(true, false)
                : GetEnumToTypes<Relationship>(true, false, (x) => _localContentProvider.SupportedRelationships.Contains(x));

            SelectContentChoicesView popup = new(input)
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
            Popup popup = PageUtilities.ShowContentLoadingPopup(this);
            
            string[] contentLines = await GetNewContentAsync(options, CancellationToken.None);
            string? newMessage = contentLines.Any()
                ? contentLines.Aggregate((l, r) => $"{l}{Environment.NewLine}{r}")
                : null;

            _viewModel.Message = newMessage ?? "Something went wrong, please try again!";
            _viewModel.EnableContentRefresh = !string.IsNullOrEmpty(_viewModel.Message);

            PageUtilities.CloseContentLoadingPopup(popup);
        }

        private Task<string[]> GetNewContentAsync(ContentOptions options, CancellationToken cancellationToken)
        {
            ApiRequest request = new()
            {
                Category = options.Category,
                To = options.Receiver,
                Type = options.Type
            };

            return _contentService.GetContentAsync(request, cancellationToken);
        }

        private async Task TrySignInAsync(CancellationToken cancellationToken)
        {
            UserContext? userContext = await _authService.SigninAsync(false, cancellationToken);
            _viewModel.WelcomeMessage = $"Hi {userContext?.GivenName ?? "there"}, welcome to Write Wise!";
        }

        private static IEnumerable<SelectItem> GetEnumToTypes<TEnum>(bool sort = false, bool sortDesc = false, Func<TEnum, bool>? filter = null) where TEnum : Enum
        {
            Func<TEnum, bool> internalFilter = filter ?? ((x) => true);

            IEnumerable<SelectItem> items = Enum
                .GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Where(t => !"None".Equals(t.ToString()) && internalFilter(t))
                .Select(t => new SelectItem(t));

            return sort
                ? sortDesc ? items.OrderByDescending(t => t.Name) : items.OrderBy(t => t.Name)
                : items;

        }
    }
}