using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class PacMan : WebCharacter
    {
        public PacMan()
            : base("PAC-MAN", potentialScrapingNames: "Pac-Man")
        {
            DisplayName = "PAC-MAN";
        }
    }
}