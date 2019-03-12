using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class LittleMac : WebCharacter
    {
        public LittleMac()
            : base("LittleMac", "Little%20Mac", null, "Little Mac")
        {
            DisplayName = "Little Mac";
            CssKey = "smallpenis";
        }
    }
}