using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Mewtwo : WebCharacter
    {
        public Mewtwo()
            : base("Mewtwo")
        {
            CssKey = "mewtwo";
        }
    }
}