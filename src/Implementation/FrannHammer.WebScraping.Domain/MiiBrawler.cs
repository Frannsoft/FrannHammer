using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class MiiBrawler : WebCharacter
    {
        public MiiBrawler()
            : base("MiiBrawler", "Mii%20Brawler", null, "MiBrawler", "Mi Brawler", "Mii Fighters")
        {
            DisplayName = "Mii Brawler";
            CssKey = "mii";
        }
    }
}