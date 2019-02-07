using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class DarkPit : WebCharacter
    {
        public DarkPit()
            : base("DarkPit", "Dark%20Pit", null, "Pit, but edgy")
        {
            DisplayName = "Dark Pit";
        }
    }
}