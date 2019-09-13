using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Charizard : WebCharacter
    {
        public Charizard()
            : base("Charizard")
        {
            CssKey = "lizardon";
        }
    }
}