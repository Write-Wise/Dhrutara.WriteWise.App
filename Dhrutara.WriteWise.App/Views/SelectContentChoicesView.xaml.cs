using System.Collections.ObjectModel;

namespace Dhrutara.WriteWise.App.Views
{
    public partial class SelectContentChoicesView : Popup
    {
        private readonly ObservableCollection<SelectItem> _contentTypes;
        private readonly ObservableCollection<SelectItem> _contentCategories;
        private readonly ObservableCollection<SelectItem> _receiverRelationships;

        internal SelectContentChoicesView(SelectContentChoicesViewModel viewModel)
        {
            InitializeComponent();

            Options = viewModel.ContentOptions;

            _contentTypes = (viewModel.ContentTypes ?? Enumerable.Empty<SelectItem>()).ToObservableCollection();
            _contentCategories = (viewModel.ContentCategories ?? Enumerable.Empty<SelectItem>()).ToObservableCollection();
            _receiverRelationships = (viewModel.ReceiverRelationships ?? Enumerable.Empty<SelectItem>()).ToObservableCollection();

            LoadPickers();
        }

        public ContentOptions Options { get; set; }

        private void OnDoneClicked(object sender, EventArgs e)
        {
            SelectItem? selectedType = pickerType.SelectedItem as SelectItem;
            SelectItem? selectedCategory = pickerCategory.SelectedItem as SelectItem;
            SelectItem? selectedReceiverRelation = pickerReceiverRelationship.SelectedItem as SelectItem;

            ContentOptions newContentOptions = new()
            {
                Category = Enum.TryParse(selectedCategory?.Name, out ContentCategory contentCategory) ? contentCategory : ContentCategory.None,
                Receiver = Enum.TryParse(selectedReceiverRelation?.Name, out Relationship relationship) ? relationship : Relationship.None,
                Type = Enum.TryParse(selectedType?.Name, out ContentType contentType) ? contentType : ContentType.Message
            };

            Close(newContentOptions);
        }

        private void LoadPickers()
        {
            pickerType.ItemsSource = _contentTypes;
            pickerType.ItemDisplayBinding = new Binding("Display");
            pickerType.SelectedItem = _contentTypes
                .FirstOrDefault(t => t.Name.Equals(Options.Type.ToString()));
            

            pickerCategory.ItemsSource = _contentCategories;
            pickerCategory.ItemDisplayBinding = new Binding("Display");
            pickerCategory.SelectedItem = _contentCategories
                .FirstOrDefault(t => t.Name.Equals(Options.Category.ToString()));

            pickerReceiverRelationship.ItemsSource = _receiverRelationships;
            pickerReceiverRelationship.ItemDisplayBinding = new Binding("Display");
            pickerReceiverRelationship.SelectedItem = _receiverRelationships
                .FirstOrDefault(t => t.Name.Equals(Options.Receiver.ToString()));
        }
    }
}