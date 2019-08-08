using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class MiiGunner : WebCharacter
    {
        public MiiGunner()
            : base("MiiGunner", "Mii%20Gunner", null, "Mii Fighters")
        {
            DisplayName = "Mii Gunner";
            CssKey = "mii";
        }
    }
}