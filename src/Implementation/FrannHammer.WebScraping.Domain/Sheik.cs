using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Sheik : WebCharacter
    {
        public Sheik()
            : base("Sheik")
        {
            CssKey = "sheik";
        }
    }
}