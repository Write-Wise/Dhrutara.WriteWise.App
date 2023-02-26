using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Dhrutara.WriteWise.App.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            string? displayName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName();
            return displayName?? enumValue.ToString();
        }
    }
}
