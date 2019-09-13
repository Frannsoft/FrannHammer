using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Link : WebCharacter
    {
        public Link()
            : base("Link")
        {
            CssKey = "link";
        }
    }
}