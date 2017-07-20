using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class MrGameAndWatch : WebCharacter
    {
        public MrGameAndWatch()
            : base("MrGameWatch", "Game%20And%20Watch", null, "MrGameWatch", "GameWatch", "Mr. Game & Watch")
        {
            DisplayName = "Game & Watch";
        }
    }
}