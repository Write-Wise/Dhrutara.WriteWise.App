using Dhrutara.WriteWise.App.Services.Content;
using CommunityToolkit.Maui.Views;

namespace Dhrutara.WriteWise.App.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;
        private readonly ContentService _contentService;
        public MainPage(MainViewModel viewModel, ContentService contentService)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
            _contentService = contentService;
        }

        private async void OnContentDoubleTapped(object sender, TappedEventArgs e)
        {
            await Task.Yield();
        }

        private async void OnContentTapped(object sender, TappedEventArgs e)
        {
            await ShowNewContentAsync(_viewModel.NewContentOptions);
        }

        private async void OnNewContentClicked(object sender, EventArgs e)
        {
            NewContentOptionsView popup = new()
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
            string? newMessage = await GetNewContentAsync(options, CancellationToken.None);
            _viewModel.Message = newMessage ?? "Something went wrong, please try again!";
        }

        private async Task<string?> GetNewContentAsync(ContentOptions options, CancellationToken cancellationToken)
        {
            ApiRequest request = new()
            {
                Category = options.Category,
                From = options.Receiver,
                Type = options.Type
            };

            return await _contentService.GetContentAsync(request, cancellationToken);
        }
     }
}