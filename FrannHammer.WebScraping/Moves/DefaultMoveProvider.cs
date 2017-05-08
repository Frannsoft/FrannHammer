using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;

namespace FrannHammer.WebScraping.Moves
{
    public class DefaultMoveProvider : IMoveProvider
    {
        public IMove Create() => new Move();
    }
}
