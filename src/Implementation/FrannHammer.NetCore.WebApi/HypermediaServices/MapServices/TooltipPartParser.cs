using FrannHammer.Utility;
using System.Text.RegularExpressions;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class TooltipPartParser
    {
        public string GetValue(string key, string tooltipContent)
        {
            Guard.VerifyStringIsNotNullOrEmpty(key, nameof(key));

            if (string.IsNullOrEmpty(tooltipContent))
            {
                return string.Empty;
            }

            string value = string.Empty;
            var keyMatch = Regex.Match(tooltipContent, $"({key}:[^,]*)");

            if (keyMatch.Success)
            {
                value = keyMatch.Value.Replace($"{key}:", string.Empty).Replace(",", string.Empty).Trim();
            }

            return value;
        }

        public bool GetPhraseOnlyValue(string key, string tooltipContent)
        {
            var phraseOnlyMatch = Regex.Match(tooltipContent, $"({key}[^,]*)");
            return phraseOnlyMatch.Success;
        }
    }
}
