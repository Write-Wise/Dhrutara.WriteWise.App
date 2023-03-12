namespace Dhrutara.WriteWise.App.Views
{
	public partial class ContentLoading : Popup
	{
		public ContentLoading()
		{
			InitializeComponent();

			
        }

		public void StartAnimation()
		{
			contentLoadingImage.IsAnimationPlaying = true;
		}
	}
}