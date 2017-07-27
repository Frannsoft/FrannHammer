using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Shulk : WebCharacter
    {
        public Shulk()
            : base("Shulk", potentialScrapingNames: new[] { "Shulk (Ground)", "Shulk (Air)" })
        { }
    }
}