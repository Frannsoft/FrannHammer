using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Inkling : WebCharacter
    {
        public Inkling()
            : base("Inkling")
        {
            CssKey = "inkling";
        }
    }
}
