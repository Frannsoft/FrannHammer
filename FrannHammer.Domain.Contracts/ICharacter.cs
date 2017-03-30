namespace FrannHammer.Domain.Contracts
{
    public interface ICharacter : IModel
    {
        string FullUrl { get; }
        string Style { get; }
        string MainImageUrl { get; }
        string ThumbnailUrl { get; }
        string Description { get; }
        string ColorTheme { get; }
        string DisplayName { get; }
    }
}
