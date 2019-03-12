using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Zelda : WebCharacter
    {
        public Zelda()
            : base("Zelda")
        {
            CssKey = "zelda";
        }
    }
}