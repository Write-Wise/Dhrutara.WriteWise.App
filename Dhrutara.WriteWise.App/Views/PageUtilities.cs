
namespace Dhrutara.WriteWise.App.Views
{
    internal static class PageUtilities
    {
        internal static Popup ShowContentLoadingPopup(ContentPage page)
        {
            ContentLoading popup = new()
            {
                CanBeDismissedByTappingOutsideOfPopup = false,
                Color = Color.FromRgba(0, 0, 0, 0)
            };

            page.ShowPopupAsync(popup).SafeFireAndForget();
            popup.StartAnimation();

            return popup;
        }

        internal static void CloseContentLoadingPopup(Popup popup)
        {
            popup?.Close();
        }
    }
}
