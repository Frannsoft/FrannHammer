using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class YoungLink : WebCharacter
    {
        public YoungLink()
            : base("YoungLink", "Young%20Link")
        {
            DisplayName = "Young Link";
            CssKey = "yink";
        }
    }
}
