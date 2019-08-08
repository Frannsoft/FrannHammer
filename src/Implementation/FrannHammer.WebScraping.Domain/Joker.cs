using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Joker : WebCharacter
    {
        public Joker()
            : base("Joker")
        {
            CssKey = "joker";
        }
    }
}
