using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class BanjoKazooie : WebCharacter
    {
        public BanjoKazooie()
            : base("BanjoKazooie", "Banjo-Kazooie", potentialScrapingNames: "Banjo-Kazooie")
        {
            CssKey = "bear";
            DisplayName = "Banjo & Kazooie";
        }
    }
}