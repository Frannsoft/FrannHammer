using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class MegaMan : WebCharacter
    {
        public MegaMan()
            : base("MegaMan", "Mega%20Man")
        {
            DisplayName = "Mega Man";
            CssKey = "megaman";
        }
    }
}