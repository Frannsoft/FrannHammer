using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class MetaKnight : WebCharacter
    {
        public MetaKnight()
            : base("MetaKnight", "Meta%20Knight", null, "Meta Knight")
        {
            DisplayName = "Meta Knight";
        }
    }
}