using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class KingKRool : WebCharacter
    {
        public KingKRool()
            : base("KingKRool", "King-K.-Rool", null, "King K. Rool", "king_k_rool", "King Kawaii Rool")
        {
            CssKey = "krool";
        }
    }
}