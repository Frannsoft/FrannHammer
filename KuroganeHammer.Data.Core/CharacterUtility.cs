using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace KuroganeHammer.Data.Core
{
    [JsonObject]
    public class CharacterUtility
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
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
