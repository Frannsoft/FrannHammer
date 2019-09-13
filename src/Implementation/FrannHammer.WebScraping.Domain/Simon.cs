using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Simon : WebCharacter
    {
        public Simon()
            : base("Simon")
        {
            CssKey = "simon";
        }
    }
}
