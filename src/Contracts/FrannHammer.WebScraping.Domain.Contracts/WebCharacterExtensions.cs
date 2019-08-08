namespace FrannHammer.WebScraping.Domain.Contracts
{
    public static class WebCharacterExtensions
    {
        public static string GetGameForUrl(this WebCharacter character) => character.OwnerId <= 58 ? "Smash4" : "Ultimate";
    }
}
