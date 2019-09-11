using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class PiranhaPlant : WebCharacter
    {
        public PiranhaPlant()
            : base("PiranhaPlant", "Piranha%20Plant", potentialScrapingNames: "Piranha Plant")
        {
            CssKey = "potplant";
            DisplayName = "Pirahna Plant";
        }
    }
}