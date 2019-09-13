using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Fox : WebCharacter
    {
        public Fox()
            : base("Fox")
        {
            CssKey = "fox";
        }
    }
}