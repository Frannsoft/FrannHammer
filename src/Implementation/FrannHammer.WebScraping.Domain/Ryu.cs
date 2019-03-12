using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Ryu : WebCharacter
    {
        public Ryu()
            : base("Ryu")
        {
            CssKey = "ryu";
        }
    }
}