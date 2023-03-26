namespace Dhrutara.WriteWise.App.Models
{
    public class SelectItem
    {
        public SelectItem(string name, string display)
        {
            Name = name;
            Display = display;
        }

        public SelectItem(Enum enumValue) : this(enumValue.ToString(), enumValue.GetDisplayName())
        {
        }
        public string Name { get; set; }
        public string Display { get; set; }
    }
}
