using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Ness : WebCharacter
    {
        public Ness()
            : base("Ness", potentialScrapingNames: new[] { "EbolaBackThrow", "Ebola Back Throw" })
        {
            CssKey = "ebola";
        }
    }
}