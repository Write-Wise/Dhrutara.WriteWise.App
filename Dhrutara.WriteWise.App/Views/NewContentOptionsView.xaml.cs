using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace Dhrutara.WriteWise.App.Views;

public partial class NewContentOptionsView : Popup
{
    private readonly ObservableCollection<EnumToType> _contentTypes;
    private readonly ObservableCollection<EnumToType> _contentCategories;
    private readonly ObservableCollection<EnumToType> _receiverRelationships;

    public NewContentOptionsView()
	{
		InitializeComponent();
        _contentTypes = GetEnumToTypes<ContentType>(true).ToObservableCollection();
        _contentCategories = GetEnumToTypes<ContentCategory>(true).ToObservableCollection();
        _receiverRelationships = GetEnumToTypes<Relationship>(true).ToObservableCollection();

        LoadPickers();
    }

    public ObservableCollection<EnumToType> ContentTypes { get { return _contentTypes; } }
    public ObservableCollection<EnumToType> ContentCategories { get { return _contentCategories; } }
    public ObservableCollection<EnumToType> ReceiverRelationships { get { return _receiverRelationships; } }

    private void OnDoneClicked(object sender, EventArgs e)
	{
        EnumToType? selectedType = pickerType.SelectedItem as EnumToType;
        EnumToType? selectedCategory = pickerCategory.SelectedItem as EnumToType;
        EnumToType? selectedReceiverRelation = pickerReceiverRelationship.SelectedItem as EnumToType;

        NewContentOptions newContentOptions = new()
        {
            Category = Enum.TryParse(selectedCategory?.Name, out ContentCategory contentCategory) ? contentCategory : ContentCategory.None,
            Receiver = Enum.TryParse(selectedReceiverRelation?.Name, out Relationship relationship) ? relationship : Relationship.None,
            Type = Enum.TryParse(selectedType?.Name, out ContentType contentType) ? contentType : ContentType.Message
        };

		Close(newContentOptions);
	}

	private void LoadPickers()
	{
        pickerType.ItemsSource = ContentTypes;
        pickerType.ItemDisplayBinding = new Binding("Display");

        pickerCategory.ItemsSource = ContentCategories;
        pickerCategory.ItemDisplayBinding = new Binding("Display");

        pickerReceiverRelationship.ItemsSource = ReceiverRelationships;
        pickerReceiverRelationship.ItemDisplayBinding = new Binding("Display");
    }

    public static IEnumerable<EnumToType> GetEnumToTypes<TEnum>(bool sort = false, bool sortDesc = false) where TEnum : Enum
    {
        var items = Enum
            .GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Where(t => !"None".Equals(t.ToString()))
            .Select(t => new EnumToType(t));

        return sort
            ? sortDesc ? items.OrderByDescending(t => t.Name) : items.OrderBy(t => t.Name)
            : items;

    }
}