using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class MoveProvider : IMoveProvider
    {
        public IMove Create() => new Move();
    }
}
