using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Ganondorf : WebCharacter
    {
        public Ganondorf()
            : base("Ganondorf")
        {
            CssKey = "ganondork";
        }
    }
}