using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class DiddyKong : WebCharacter
    {
        public DiddyKong()
            : base("DiddyKong", "Diddy%20Kong", null, "Diddy")
        {
            DisplayName = "Diddy Kong";
            CssKey = "diddy";
        }
    }
}