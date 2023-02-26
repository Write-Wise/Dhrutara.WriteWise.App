namespace Dhrutara.WriteWise.App.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _mainViewModel;
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _mainViewModel = viewModel;
        }

        private async void OnContentDoubleTapped(object sender, TappedEventArgs e)
        {
            await Task.Yield();
        }

        private async void OnContentTapped(object sender, TappedEventArgs e)
        {
            await _mainViewModel.ShowNewContent();
        }
    }
}