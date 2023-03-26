namespace Dhrutara.WriteWise.App.ViewModels
{
    internal class SelectContentChoicesViewModel
    {
        public SelectContentChoicesViewModel(ContentOptions contentOptions)
        {
            ContentOptions = contentOptions;
        }
        public ContentOptions ContentOptions { get; set; }
        public IEnumerable<SelectItem> ContentTypes { get; set; } = Enumerable.Empty<SelectItem>();
        public IEnumerable<SelectItem> ContentCategories { get; set; } = Enumerable.Empty<SelectItem>();
        public IEnumerable<SelectItem> ReceiverRelationships { get; set; } = Enumerable.Empty<SelectItem>();
    }
}
