using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Marth : WebCharacter
    {
        public Marth()
            : base("Marth", potentialScrapingNames: "Lucina\'s worthless grandfather")
        {
            CssKey = "marth";
        }
    }
}