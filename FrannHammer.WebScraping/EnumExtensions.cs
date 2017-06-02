using System;
using System.ComponentModel;
using System.Reflection;

namespace FrannHammer.WebScraping
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var attribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>(false);

            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
