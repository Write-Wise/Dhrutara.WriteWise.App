namespace Dhrutara.WriteWise.App.Models
{
    public class EnumToType
    {
        public EnumToType(string name, string display)
        {
            Name = name;
            Display = display;
        }

        public EnumToType(Enum enumValue) : this(enumValue.ToString(), enumValue.GetDisplayName())
        {
        }
        public string Name { get; set; }
        public string Display { get; set; }
    }
}
