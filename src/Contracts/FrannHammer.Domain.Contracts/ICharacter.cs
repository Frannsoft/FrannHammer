namespace FrannHammer.Domain.Contracts
{
    public interface ICharacter : IModel, IHaveAnOwnerId
    {
        string FullUrl { get; set; }
        string MainImageUrl { get; set; }
        string ThumbnailUrl { get; set; }
        string ColorTheme { get; set; }
        string DisplayName { get; set; }
    }
}
