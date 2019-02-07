using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class DrMario : WebCharacter
    {
        public DrMario()
            : base("DrMario", "Dr.%20Mario", null, "PhDMario", "Ph.D. Mario", "Educated Mario")
        {
            DisplayName = "Dr. Mario";
        }
    }
}