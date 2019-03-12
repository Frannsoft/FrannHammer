using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class ZeroSuitSamus : WebCharacter
    {
        public ZeroSuitSamus()
            : base("ZeroSuitSamus", "Zero%20Suit%20Samus")
        {
            DisplayName = "Zero Suit Samus";
            CssKey = "zerosuit";
        }
    }
}