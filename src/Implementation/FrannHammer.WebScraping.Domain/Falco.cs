using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Falco : WebCharacter
    {
        public Falco()
            : base("Falco")
        {
            CssKey = "falco";
        }
    }
}