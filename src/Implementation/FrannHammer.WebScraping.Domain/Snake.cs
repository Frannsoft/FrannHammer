using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Snake : WebCharacter
    {
        public Snake()
            : base("Snake")
        {
            CssKey = "snake";
        }
    }
}
