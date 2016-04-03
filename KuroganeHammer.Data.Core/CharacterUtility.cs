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

        public static int GetCharacterIdFromName(string characterName)
        {
            var characterId = (Characters)Enum.Parse(typeof(Characters), characterName, true);
            return (int)characterId;
        }
    }
}
