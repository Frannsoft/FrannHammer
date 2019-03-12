using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Mario : WebCharacter
    {
        public Mario()
            : base("Mario")
        {
            CssKey = "mario";
        }
    }
}