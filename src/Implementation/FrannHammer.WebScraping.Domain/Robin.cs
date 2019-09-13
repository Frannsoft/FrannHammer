using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Robin : WebCharacter
    {
        public Robin()
            : base("Robin")
        {
            CssKey = "magicmarth";
        }
    }
}