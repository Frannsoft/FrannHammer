using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class ToonLink : WebCharacter
    {
        public ToonLink()
            : base("ToonLink", "Toon%20Link")
        {
            DisplayName = "Toon Link";
            CssKey = "toonlink";
        }
    }
}