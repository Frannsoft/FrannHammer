using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Olimar : WebCharacter
    {
        public Olimar()
            : base("Olimar")
        {
            CssKey = "olimar";
        }
    }
}