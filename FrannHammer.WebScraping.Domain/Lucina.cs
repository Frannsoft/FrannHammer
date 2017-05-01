using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Lucina : WebCharacter
    {
        public Lucina()
            : base("Lucina", potentialScrapingNames: "Marth\'s worthless granddaughter")
        { }
    }
}