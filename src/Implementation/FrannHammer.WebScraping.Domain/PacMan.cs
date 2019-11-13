using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class PacMan : WebCharacter
    {
        public PacMan()
            : base("PAC-MAN", potentialScrapingNames: "Pac-Man")
        {
            DisplayName = "PAC-MAN";
            CssKey = "pacman";
        }
    }

    public class Terry : WebCharacter
    {
        public Terry()
            : base("Terry", potentialScrapingNames: "terry")
        {
            DisplayName = "Terry";
            CssKey = "terry";
        }
    }
}