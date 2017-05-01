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

            var attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes<DescriptionAttribute>(false);

            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
