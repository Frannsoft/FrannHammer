using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class BanjoKazooie : WebCharacter
    {
        public BanjoKazooie()
            : base("Banjo-Kazooie")
        {
            CssKey = "bear";
        }
    }
}