using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace API.Utilities
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
            {
                return displayAttribute.Name;
            }

            // Jika tidak ada atribut DisplayAttribute, kembalikan nama enum default
            return value.ToString();
        }
    }
}
