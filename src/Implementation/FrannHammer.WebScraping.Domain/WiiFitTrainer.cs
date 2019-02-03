using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class WiiFitTrainer : WebCharacter
    {
        public WiiFitTrainer()
            : base("WiiFitTrainer", "Wii%20Fit%20Trainer", potentialScrapingNames: "Wii FIt Trainer")
        {
            DisplayName = "Wii Fit Trainer";
        }
    }
}