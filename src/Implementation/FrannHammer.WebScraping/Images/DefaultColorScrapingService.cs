using ExCSS;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Images;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FrannHammer.WebScraping.Images
{
    public class DefaultColorScrapingService : IColorScrapingService
    {
        private readonly string _characterCss;

        public DefaultColorScrapingService(string characterCss)
        {
            Guard.VerifyStringIsNotNullOrEmpty(characterCss, nameof(characterCss));
            _characterCss = characterCss;
        }

        public async Task<string> GetColorHexValue(string characterName)
        {
            var stylesheet = await new StylesheetParser().ParseAsync(_characterCss);

            var background = stylesheet.StyleRules
                .First(r =>
                {
                    var rule = (StyleRule)r;

                    return rule.SelectorText == $".{characterName} table th" && !string.IsNullOrEmpty(rule.Style.Background);
                });

            string actualRgb = ((StyleRule)background).Style.Background;

            string vals = actualRgb.Split('(')[1];

            string justVals = actualRgb
                .Replace("rgb(", string.Empty)
                .Replace(")", string.Empty)
                .Replace(" ", string.Empty);

            var rgbs = justVals.Split(',');

            var color = Color.FromRgb(Convert.ToByte(rgbs[0]), Convert.ToByte(rgbs[1]), Convert.ToByte(rgbs[2]));

            string colorAsHex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";

            return colorAsHex;
        }
    }
}
