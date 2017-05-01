using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Greninja : WebCharacter
    {
        public Greninja()
            : base("Greninja", potentialScrapingNames: new[] { "GreninjaForward", "GreninjaBack" })
        { }
    }
}
