using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class IceClimbers : WebCharacter
    {
        public IceClimbers()
            : base("IceClimbers", "Ice-Climbers", potentialScrapingNames: "Ice Climbers")
        {
            CssKey = "nana";
            DisplayName = "Ice Climbers";
        }
    }
}