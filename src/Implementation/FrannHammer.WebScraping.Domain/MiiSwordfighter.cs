using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class MiiSwordfighter : WebCharacter
    {
        public MiiSwordfighter()
            : base("MiiSwordfighter", "Mii%20Swordfighter", null, "MiiSwordspider", "Mii Fighters")
        {
            DisplayName = "Mii Swordfighter";
        }
    }
}