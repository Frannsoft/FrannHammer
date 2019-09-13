using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Bayonetta : WebCharacter
    {
        public Bayonetta()
            : base("Bayonetta")
        {
            CssKey = "bazza";
        }
    }
}