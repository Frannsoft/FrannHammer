using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Luigi : WebCharacter
    {
        public Luigi()
            : base("Luigi")
        {
            CssKey = "luigiu";
        }
    }
}