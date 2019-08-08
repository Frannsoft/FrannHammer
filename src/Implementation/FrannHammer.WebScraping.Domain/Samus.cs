using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Samus : WebCharacter
    {
        public Samus()
            : base("Samus")
        {
            CssKey = "samus";
        }
    }
}