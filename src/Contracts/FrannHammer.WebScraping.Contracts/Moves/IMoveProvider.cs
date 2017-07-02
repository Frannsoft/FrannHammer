using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Moves
{
    public interface IMoveProvider
    {
        IMove Create();
    }
}
