using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Ike : WebCharacter
    {
        public Ike()
            : base("Ike")
        {
            CssKey = "ike";
        }
    }
}